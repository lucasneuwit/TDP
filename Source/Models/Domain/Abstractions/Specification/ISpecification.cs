namespace TDP.Models.Domain;

public interface ISpecification<T>
where T : class
{
    IQueryable<T> Apply(IQueryable<T> queryable);
}