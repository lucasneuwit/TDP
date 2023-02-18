using Microsoft.EntityFrameworkCore;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly TdpDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;

    public Repository(TdpDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbSet = this.dbContext.Set<TEntity>();
    }
    
    public Task CreateAsync(TEntity entity)
    { 
        return dbSet.AddAsync(entity).AsTask();
    }

    public Task UpdateAsync(TEntity entity)
    {
        return dbSet.Update(entity).ReloadAsync();
    }

    public Task<TEntity?> FindByIdAsync(Guid id)
    {
        return dbSet.FindAsync().AsTask();
    }

    public async Task<IEnumerable<TEntity>> AllAsync(ISpecification<TEntity> specification)
    {
        return specification.Apply(this.dbSet).ToList();
    }
}