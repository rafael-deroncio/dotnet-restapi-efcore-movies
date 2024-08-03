using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using MovieMania.Core.Configurations.DTOs;
using MovieMania.Core.Extensions;
using MovieMania.Core.Repositories.Interfaces;

namespace MovieMania.Core.Repositories;

public class BaseRepository<TEntity>(DbContext context, IDatabaseMemory memory = null) : IBaseRepository<TEntity> where TEntity : EntityBase
{
    private readonly DbContext _context = context;
    private readonly DbSet<TEntity> _entity = context.GetEntity<TEntity>();
    private readonly IDatabaseMemory _memory = memory;

    public async Task<TEntity> Get(TEntity entity)
    {
        IProperty property = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.FirstOrDefault()
            ?? throw new InvalidOperationException("No primary key defined for entity.");

        object id = property.PropertyInfo.GetValue(entity);

        return _memory is not null ? GetDatabaseMemoryFist(id) ?? await _entity.FindAsync(id) ?? null : null;
    }

    public async Task<IEnumerable<TEntity>> Get()
        =>  await Task.FromResult(_memory is not null ? GetDatabaseMemoryFist() ?? _entity.AsEnumerable() : null);

    public async Task<TEntity> Create(TEntity entity)
    {
        entity.UpdatedAt = entity.CreatedAt = DateTime.UtcNow;
        EntityEntry<TEntity> result = await _entity.AddAsync(entity);
        await _context.SaveChangesAsync();
        if (result.Entity is not null)
        {
            await UpdateEntityInDatabaseMemory();
            return result.Entity;
        }
        return null;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        EntityEntry<TEntity> result = _entity.Update(entity);
        if (result.Entity is not null)
        {
            await UpdateEntityInDatabaseMemory();
            return result.Entity;
        }
        return null;
    }

    public async Task<bool> Delete(TEntity entity)
    {
        EntityEntry<TEntity> result = _entity.Remove(entity);
        await _context.SaveChangesAsync();
        if (result.Entity is not null)
        {
            await UpdateEntityInDatabaseMemory();
            return true;
        }
        return false;
    }

    public async Task<int> Count()
        => _memory is not null ? await Task.FromResult(GetDatabaseMemoryFist().Count()) : await _entity.CountAsync();

    public async Task<IEnumerable<TEntity>> Paged(int page, int size)
        => await _entity.Skip((page - 1) * size).Take(size).ToListAsync()
            ?? new List<TEntity>().AsEnumerable();

    private string GetPropertyDbSet()
    {
        PropertyInfo[] properties = _context.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType == typeof(DbSet<TEntity>))
                return property.Name;
        }

        throw new ArgumentException($"No DbSet property found for entity type {typeof(TEntity).Name}");
    }

    private TEntity GetDatabaseMemoryFist(object id)
    {
        PropertyInfo memoryProperty = _memory.GetType().GetProperty(GetPropertyDbSet());
        if (memoryProperty is not null && memoryProperty.GetValue(_memory) is IEnumerable<TEntity> memoryDbSet)
        {
            string idProperty = typeof(TEntity).Name.Replace("Entity", "Id", StringComparison.CurrentCultureIgnoreCase);
            PropertyInfo entityIdProperty = typeof(TEntity).GetProperty(idProperty);
            if (entityIdProperty != null)
            {
                TEntity result = memoryDbSet.FirstOrDefault(GetLambda(id, entityIdProperty).Compile());
                if (result != null)
                    return result;
            }
        }

        return null;
    }

    private IEnumerable<TEntity> GetDatabaseMemoryFist()
    {
        PropertyInfo memoryProperty = _memory.GetType().GetProperty(GetPropertyDbSet());
        if (memoryProperty is not null && memoryProperty.GetValue(_memory) is IEnumerable<TEntity> memoryDbSet)
            return memoryDbSet.AsEnumerable();

        return new List<TEntity>().AsEnumerable();
    }

    private async Task UpdateEntityInDatabaseMemory()
    {
        string methodName = $"Update{GetPropertyDbSet()}";
        MethodInfo memoryMethod = _memory.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

        if (memoryMethod is not null)
            await (Task)memoryMethod.Invoke(_memory, null);
    }

    private static Expression<Func<TEntity, bool>> GetLambda(object id, PropertyInfo entityIdProperty)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
        MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, entityIdProperty);
        ConstantExpression constant = Expression.Constant(id);
        BinaryExpression equality = Expression.Equal(propertyAccess, constant);
        return Expression.Lambda<Func<TEntity, bool>>(equality, parameter);
    }

}
