namespace TDP.Models.Domain;

/// <summary>
/// Base type to be implemented by all entities.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the entity id.
    /// </summary>
    public Guid Id { get; set; }
}