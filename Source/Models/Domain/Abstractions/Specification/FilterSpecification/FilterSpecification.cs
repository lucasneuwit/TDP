using System.Linq.Expressions;

namespace TDP.Models.Domain;

public abstract class FilterSpecification<T> : IFilterSpecification<T>
    where T : class
{
    public Expression<Func<T, bool>> expression { get; init; } = arg => true ;

    public bool IsSatisfied(T obj)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Apply(IQueryable<T> queryable)
    {
        return queryable.Where(this.expression);
    }
}