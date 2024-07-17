namespace TDP.Models.Domain;

public class EntityByIdFilterSpecification<TEntity> : FilterSpecification<TEntity>
    where TEntity : class, IEntity
{
    public EntityByIdFilterSpecification(Guid userId)
    {
        this.expression = x => x.Id == userId;
    }
}