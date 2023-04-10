using TDP.Models.Domain;
using TDP.Models.Domain.Abstractions;

namespace TDP.Models.Persistence.Repository.Factory;

public interface IRepositoryFactory
{
    IRepository<TEntity> GetRepository<TEntity>(IServiceProvider serviceProvider) where TEntity : BaseEntity;
}

public class RepositoryFactory : IRepositoryFactory
{
    public IRepository<TEntity> GetRepository<TEntity>(IServiceProvider serviceProvider) where TEntity : BaseEntity
    {
        var dbContext = serviceProvider.GetRequiredService<TdpDbContext>();
        var genericRepositoryType = typeof(Repository<>);
        var typedRepository = genericRepositoryType.MakeGenericType(typeof(TEntity));
        return (IRepository<TEntity>)ActivatorUtilities.CreateInstance(serviceProvider, typedRepository, dbContext);
    }
}