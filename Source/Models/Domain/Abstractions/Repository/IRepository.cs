namespace TDP.Models.Domain;

public interface IRepository<TEntity> where TEntity: class
{
    Task CreateAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task<TEntity?> FindByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> AllAsync(ISpecification<TEntity> specification);
}