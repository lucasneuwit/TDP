namespace TDP.Models.Domain;

/// <inheritdoc />
public abstract class BaseEntity : IEntity
{
    protected BaseEntity(Guid id)
    {
        this.Id = id;
    }
    
    public Guid Id { get; set; }
}