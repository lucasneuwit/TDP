namespace TDP.Models.Persistence;

public class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly TdpDbContext dbContext;
    private readonly ILogger<IUnitOfWorkManager> logger;

    public UnitOfWorkManager(TdpDbContext dbContext, ILogger<IUnitOfWorkManager> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    private IUnitOfWork? current;
    
    public void BeginUnitOfWork()
    {
        this.current = new UnitOfWork(this.dbContext);
        this.logger.LogDebug("Starting new unit of work: {unitOfWorkHash}", this.current.GetHashCode());
        this.current.BeginTransactionAsync();
    }

    public async Task Complete()
    {
        if (this.current != null)
        {
            await this.current?.CommitTransactionAsync();
            await this.current?.CompleteAsync();
        }
        
        this.logger.LogDebug("Unit of work completed: {unitOfWorkHash}", this.current?.GetHashCode());
    }
}