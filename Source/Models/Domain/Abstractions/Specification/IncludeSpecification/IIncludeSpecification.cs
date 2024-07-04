using System.Linq.Expressions;

namespace TDP.Models.Domain;

public interface IIncludeSpecification<T> : ISpecification<T>
    where T : class

{
    void Include<TProperty>(Expression<Func<T, TProperty>> selector);
}