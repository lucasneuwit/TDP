using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TDP.Models.Domain;

public abstract class IncludeSpecification<T> : IIncludeSpecification<T>
    where T : class

{
    private ICollection<string> Includes { get; } = new HashSet<string>();
    
    public IQueryable<T> Apply(IQueryable<T> queryable)
    {
        return this.Includes.Aggregate(queryable, (current, include) => current.Include(include));
    }

    public void Include<TProperty>(Expression<Func<T, TProperty>> selector)
    {
        var visitor = new IncludeVisitor();
        visitor.Visit(selector);

        this.Includes.Add(visitor.Path);
    }
}