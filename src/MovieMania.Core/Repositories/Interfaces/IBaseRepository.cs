using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : EntityBase 
{
    Task<TEntity> Get(TEntity entity);
    Task<TEntity> Create(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Delete(TEntity entity);
    Task<IEnumerable<TEntity>> Paged(int page, int size);
    Task<int> Count();
}
