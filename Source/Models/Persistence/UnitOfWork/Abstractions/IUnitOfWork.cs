namespace TDP.Models.Persistence;

/// <summary>
/// The unit of work manages the transactions against the application storage.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);}