namespace TDP.Models.Domain;

public class AggregateSpecification<T> : ISpecification<T>
    where T : class
{
    private ICollection<ISpecification<T>> specifications = new List<ISpecification<T>>();
    
    public IQueryable<T> Apply(IQueryable<T> queryable)
    {
        return this.specifications.Aggregate(queryable, (current, specification) => specification.Apply(current));
    }

    public AggregateSpecification<T> AddFilter(IFilterSpecification<T> filterSpecification)
    {
        this.specifications.Add(filterSpecification);
        return this;
    }

    public AggregateSpecification<T> AddInclude(IIncludeSpecification<T> includeSpecification)
    {
        this.specifications.Add(includeSpecification);
        return this;
    }
}