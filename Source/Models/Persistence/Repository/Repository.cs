using Microsoft.EntityFrameworkCore;
using TDP.Models.Domain;
using TDP.Models.Domain.Abstractions;

namespace TDP.Models.Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly TdpDbContext dbContext;

    public Repository(TdpDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(TEntity entity)
    {
        var dbSet = this.GetDbSet();
        await dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
        var dbSet = this.GetDbSet();
        return Task.FromResult(dbSet.Update(entity));
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        var dbSet = this.GetDbSet();
        return await dbSet.FindAsync(id);
    }

    public Task<IEnumerable<TEntity>> AllAsync(ISpecification<TEntity> specification)
    {
        var dbSet = this.GetDbSet();
        return Task.FromResult(dbSet.AsEnumerable());
    }

    private DbSet<TEntity> GetDbSet()
    {
        return dbContext.Set<TEntity>();
    }
}