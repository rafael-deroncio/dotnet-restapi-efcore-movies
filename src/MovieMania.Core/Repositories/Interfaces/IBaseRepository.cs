using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Repositories.Interfaces;

/// <summary>
/// Interface for basic repository operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Retrieves an entity.
    /// </summary>
    /// <param name="entity">The entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the retrieved entity.</returns>
    Task<TEntity> Get(TEntity entity);

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains an enumerable collection of entities.
    /// </returns>
    Task<IEnumerable<TEntity>> Get();

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the created entity.</returns>
    Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the updated entity.</returns>
    Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> Delete(TEntity entity);

    /// <summary>
    /// Retrieves a paginated list of entities.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="size">The number of items per page.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains an enumerable of entities.</returns>
    Task<IEnumerable<TEntity>> Paged(int page, int size);

    /// <summary>
    /// Counts the total number of entities.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains the total count of entities.</returns>
    Task<int> Count();
}

