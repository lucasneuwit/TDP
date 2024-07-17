namespace TDP.Models.Domain;

public abstract class BaseEntity : IEntity
{
    protected BaseEntity(Guid id)
    {
        this.Id = id;
    }
    
    public Guid Id { get; set; }
}