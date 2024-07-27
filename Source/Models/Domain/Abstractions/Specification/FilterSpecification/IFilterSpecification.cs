namespace TDP.Models.Domain;

/// <summary>
/// Allows to apply a filtering condition to be used when querying an entity.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IFilterSpecification<T> : ISpecification<T>
    where T : class

{
    /// <summary>
    /// Validates if the given entity satisfies the specification condition.
    /// </summary>
    /// <param name="obj">The entity to be evaluated.</param>
    /// <returns>A value indicating whether the entity satisfies the condition.</returns>
    bool IsSatisfied(T obj);
}