namespace TDP.Models.Persistence.UnitOfWork;

public interface IUnitOfWorkManager
{
    void BeginUnitOfWork();

    Task Complete();
}

public class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly TdpDbContext dbContext;

    public UnitOfWorkManager(TdpDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private IUnitOfWork? current;
    
    public void BeginUnitOfWork()
    {
        this.current = new UnitOfWork(this.dbContext);
        this.current.BeginTransactionAsync();
    }

    public async Task Complete()
    {
        await this.current?.CommitTransactionAsync();
        await this.current?.CompleteAsync();
    }
}