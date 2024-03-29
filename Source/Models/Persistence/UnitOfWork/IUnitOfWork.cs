﻿namespace TDP.Models.Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);}