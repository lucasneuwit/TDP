using Microsoft.EntityFrameworkCore.Storage;

namespace TDP.Models.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly TdpDbContext dbContext;

    private IDbContextTransaction? transaction;

    public UnitOfWork(TdpDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Dispose()
    {
        this.transaction?.Dispose();
        this.dbContext.Dispose();
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        this.transaction = await this.dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await this.transaction?.CommitAsync(cancellationToken);
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}