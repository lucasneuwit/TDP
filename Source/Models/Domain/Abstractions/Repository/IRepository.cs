using TDP.Models.Persistence;

namespace TDP.Models.Domain;

public interface IRepository<TEntity> where TEntity: class
{
    /// <summary>
    /// Stores an entity.
    /// </summary>
    /// <param name="entity">The entity to be created</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task CreateAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Searches for an entity by its id.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <returns>The specified entity, if found.</returns>
    Task<TEntity?> FindByIdAsync(Guid id);

    /// <summary>
    /// Searches for an entity by its id.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <param name="includeSpecification">A <see cref="IIncludeSpecification{T}"/> for data aggregation.</param>
    /// <returns>The specified entity, if found.</returns>
    Task<TEntity?> FindByIdAsync(Guid id, IIncludeSpecification<TEntity> includeSpecification);

    /// <summary>
    /// Searches for an entity by its id.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <returns>The specified entity.</returns>
    /// <exception cref="EntityNotFoundException"> if no entity with the specified id was found.</exception>
    Task<TEntity> FindByIdOrThrowAsync(Guid id);
    
    /// <summary>
    /// Searches for an entity by its id.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <param name="includeSpecification">A <see cref="IIncludeSpecification{T}"/> for data aggregation.</param>
    /// <returns>The specified entity.</returns>
    /// <exception cref="EntityNotFoundException"> if no entity with the specified id was found.</exception>
    Task<TEntity> FindByIdOrThrowAsync(Guid id, IIncludeSpecification<TEntity> includeSpecification);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Gets all entities that match the query filter condition.
    /// </summary>
    /// <param name="specification">A <see cref="ISpecification{T}"/> instance with the query conditions.</param>
    /// <returns>A list of entities matching the filter criteria.</returns>
    Task<IEnumerable<TEntity>> AllAsync(ISpecification<TEntity>? specification = null);

    /// <summary>
    /// Evaluates if a certain condition is matched by at least one entity.
    /// </summary>
    /// <param name="specification">A <see cref="IFilterSpecification{T}"/> with the filter criteria.</param>
    /// <returns>A value indicating whether there is any entity matching the given condition.</returns>
    Task<bool> AnyAsync(IFilterSpecification<TEntity> specification);

    /// <summary>
    /// Finds the first entity that matches the query filter condition.
    /// </summary>
    /// <param name="specification">A <see cref="ISpecification{T}"/> with the query conditions.</param>
    /// <returns>The entity instance if found. Null otherwise.</returns>
    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification);
}