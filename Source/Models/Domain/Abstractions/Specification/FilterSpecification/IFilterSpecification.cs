namespace TDP.Models.Domain;

public interface IFilterSpecification<T> : ISpecification<T>
    where T : class

{
    bool IsSatisfied(T obj);
}