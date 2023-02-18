namespace TDP.Models.Domain;

public interface ISpecification<T>
{
    IQueryable<T> Apply(IQueryable<T> queryable);
    
    bool IsSatisfied(T obj);
}