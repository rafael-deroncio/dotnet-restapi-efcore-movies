using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using MovieMania.Core.Configurations.DTOs;
using MovieMania.Core.Extensions;
using MovieMania.Core.Repositories.Interfaces;

namespace MovieMania.Core.Repositories;

public class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity> where TEntity : EntityBase
{
    private readonly DbContext _context = context;
    private readonly DbSet<TEntity> _entity = context.GetEntity<TEntity>();

    public async Task<TEntity> Get(TEntity entity)
    {
        IProperty property = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.FirstOrDefault()
            ?? throw new InvalidOperationException("No primary key defined for entity.");

        return await _entity.FindAsync(property.PropertyInfo.GetValue(entity)) ?? null;
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        entity.UpdatedAt = entity.CreatedAt = DateTime.UtcNow;
        EntityEntry<TEntity> result = await _entity.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity ?? null;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        EntityEntry<TEntity> result = _entity.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity ?? null;
    }

    public async Task<bool> Delete(TEntity entity)
    {
        EntityEntry<TEntity> result = _entity.Remove(entity);
        await _context.SaveChangesAsync();
        return result.Entity != null;
    }

    public async Task<int> Count()
        => await _entity.CountAsync();

    public async Task<IEnumerable<TEntity>> Paged(int page, int size)
        => await _entity.Skip((page - 1) * size).Take(size).ToListAsync() ?? null;
}
