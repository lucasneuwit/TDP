using System.Linq.Expressions;

namespace TDP.Models.Domain.Specification;

public abstract class FilterSpecification<T> : ISpecification<T>
{
    private readonly Expression<Func<T, bool>> expression;

    protected FilterSpecification(Expression<Func<T, bool>> expression)
    {
        this.expression = expression;
    }

    public bool IsSatisfied(T obj)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Apply(IQueryable<T> queryable)
    {
        return queryable.Where(this.expression);
    }
}