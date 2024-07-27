namespace TDP.Models.Domain;

/// <summary>
/// Base interface to be implemented by specifications.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISpecification<T>
where T : class
{
    /// <summary>
    /// Applies a condition against an <see cref="IQueryable{T}"/> instance.
    /// </summary>
    /// <param name="queryable">The <see cref="IQueryable{T}"/> instance.</param>
    /// <returns>The resulting <see cref="IQueryable{T}"/>.</returns>
    IQueryable<T> Apply(IQueryable<T> queryable);
}