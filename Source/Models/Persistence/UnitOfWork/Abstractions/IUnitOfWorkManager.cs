namespace TDP.Models.Persistence;

/// <summary>
/// Manages the <see cref="IUnitOfWork"/> instance used during the transaction lifetime.
/// </summary>
public interface IUnitOfWorkManager
{
    /// <summary>
    /// Begins a new <see cref="IUnitOfWork"/>.
    /// </summary>
    void BeginUnitOfWork();

    /// <summary>
    /// Completes the current <see cref="IUnitOfWork"/>, if any.
    /// </summary>
    /// <returns></returns>
    Task Complete();
}