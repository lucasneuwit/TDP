namespace TDP.Models.Domain.Abstractions;

public abstract class BaseEntity : IEntity
{
    protected BaseEntity(Guid id)
    {
        this.Id = id;
    }
    
    public Guid Id { get; set; }
}