using TDP.Models.Domain.Abstractions;
using TDP.Models.Domain.Specification;

namespace TDP.Models.Domain.Specifications;

public class EntityByIdFilterSpecification<TEntity> : FilterSpecification<TEntity>
    where TEntity : class, IEntity
{
    public EntityByIdFilterSpecification(Guid userId)
    {
        this.expression = x => x.Id == userId;
    }
}