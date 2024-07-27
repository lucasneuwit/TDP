using System.Linq.Expressions;

namespace TDP.Models.Domain;

/// <summary>
/// Allows to include relationships when querying a given entity.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IIncludeSpecification<T> : ISpecification<T>
    where T : class

{
    /// <summary>
    /// Specifies the navigation property to include.
    /// </summary>
    /// <param name="selector">A selection expression for the related entity.</param>
    /// <typeparam name="TProperty">The related property type.</typeparam>
    void Include<TProperty>(Expression<Func<T, TProperty>> selector);
}