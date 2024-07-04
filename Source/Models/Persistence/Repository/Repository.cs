using Microsoft.EntityFrameworkCore;
using TDP.Models.Domain;
using TDP.Models.Domain.Abstractions;
using TDP.Models.Domain.Specifications;

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

    public Task<TEntity?> FindByIdAsync(Guid id, IIncludeSpecification<TEntity> includeSpecification)
    {
        var aggregateSpec = new AggregateSpecification<TEntity>();
        aggregateSpec.AddFilter(new EntityByIdFilterSpecification<TEntity>(id));
        aggregateSpec.AddInclude(includeSpecification);

        return this.FirstOrDefaultAsync(aggregateSpec);
    }

    public async Task<TEntity> FindByIdOrThrowAsync(Guid id)
    {
        var entity = await this.FindByIdAsync(id);
        if (entity is null)
        {
            throw new EntityNotFoundException(id);
        }

        return entity;
    }

    public async Task<TEntity> FindByIdOrThrowAsync(Guid id, IIncludeSpecification<TEntity> includeSpecification)
    {
        var entity = await this.FindByIdAsync(id, includeSpecification);
        if (entity is null)
        {
            throw new EntityNotFoundException(id);
        }

        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbSet = this.GetDbSet();
        var entity = await dbSet.FindAsync(id);
        if (entity is not null)
        {
            dbSet.Remove(entity);
        }
    }

    public Task<IEnumerable<TEntity>> AllAsync(ISpecification<TEntity>? specification)
    {
        var dbSet = this.GetDbSet();
        return Task.FromResult(dbSet.AsEnumerable());
    }

    public Task<bool> AnyAsync(ISpecification<TEntity> specification)
    {
        var dbSet = this.GetDbSet();
        return specification.Apply(dbSet).AnyAsync();
    }

    public Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification)
    {
        var dbSet = this.GetDbSet();
        return specification.Apply(dbSet).FirstOrDefaultAsync();
    }

    private DbSet<TEntity> GetDbSet()
    {
        return dbContext.Set<TEntity>();
    }
}

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Guid entityId) : base($"Entity with id: {entityId} not found")
    {
    }
}