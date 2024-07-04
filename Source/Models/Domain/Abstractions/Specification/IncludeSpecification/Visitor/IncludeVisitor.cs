using System.Linq.Expressions;

namespace TDP.Models.Domain;

public class IncludeVisitor : ExpressionVisitor
{
    public string Path { get; private set; } = null!;
    
    protected override Expression VisitMember(MemberExpression node)
    {
        this.Path = node.Member.Name;
        return base.VisitMember(node);
    }
}