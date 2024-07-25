using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieMania.Core.Configurations.DTOs;
using MovieMania.Core.Extensions;
using MovieMania.Core.Repositories.Interfaces;

namespace MovieMania.Core.Repositories;

public class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity> where TEntity : EntityBase
{
    private readonly DbContext _context = context;
    private readonly DbSet<TEntity> _entity = context.GetEntity<TEntity>();

    public async Task<TEntity> Get(TEntity entity)
        => await _entity.FindAsync(entity);

    public async Task<TEntity> Create(TEntity entity)
    {
        EntityEntry<TEntity> result = await _entity.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        EntityEntry<TEntity> result = _entity.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<TEntity> Delete(TEntity entity)
    {
        EntityEntry<TEntity> result = _entity.Remove(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<int> Count()
        => await _entity.CountAsync();

    public async Task<IEnumerable<TEntity>> Paged(int page, int size)
        => await _entity.Skip((page - 1) * size).Take(size).ToListAsync();
}
