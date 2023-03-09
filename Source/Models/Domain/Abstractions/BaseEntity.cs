namespace TDP.Models.Domain.Abstractions;

public abstract class BaseEntity
{
    protected BaseEntity(Guid id)
    {
        this.Id = id;
    }
    
    public Guid Id { get; set; }
}