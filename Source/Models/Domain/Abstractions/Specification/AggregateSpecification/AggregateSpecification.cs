namespace TDP.Models.Domain;

/// <summary>
/// Allows to aggregate several
/// <see cref="IFilterSpecification{T}"/> and <see cref="IIncludeSpecification{T}"/> instances
/// to be used when querying entities.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public class AggregateSpecification<T> : ISpecification<T>
    where T : class
{
    private ICollection<ISpecification<T>> specifications = new List<ISpecification<T>>();
    
    public IQueryable<T> Apply(IQueryable<T> queryable)
    {
        return this.specifications.Aggregate(queryable, (current, specification) => specification.Apply(current));
    }

    /// <summary>
    /// Adds a <see cref="IFilterSpecification{T}"/> to the aggregate.
    /// </summary>
    /// <param name="filterSpecification">The <see cref="IFilterSpecification{T}"/> instance.</param>
    /// <returns>The resulting <see cref="AggregateSpecification{T}"/>.</returns>
    public AggregateSpecification<T> AddFilter(IFilterSpecification<T> filterSpecification)
    {
        this.specifications.Add(filterSpecification);
        return this;
    }

    /// <summary>
    /// Adds a <see cref="IIncludeSpecification{T}"/> to the aggregate.
    /// </summary>
    /// <param name="includeSpecification">The <see cref="IIncludeSpecification{T}"/> instance.</param>
    /// <returns>The resulting <see cref="AggregateSpecification{T}"/>.</returns>
    public AggregateSpecification<T> AddInclude(IIncludeSpecification<T> includeSpecification)
    {
        this.specifications.Add(includeSpecification);
        return this;
    }
}