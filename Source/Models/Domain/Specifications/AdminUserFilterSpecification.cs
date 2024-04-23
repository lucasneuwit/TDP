using System.Linq.Expressions;
using TDP.Models.Domain.Specification;

namespace TDP.Models.Domain.Specifications;

public class AdminUserFilterSpecification : FilterSpecification<User>
{
    public AdminUserFilterSpecification()
    {
        this.expression = x => x.IsAdministrator;
    }
}