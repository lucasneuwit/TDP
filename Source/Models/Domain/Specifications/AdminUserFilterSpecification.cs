namespace TDP.Models.Domain;

public class AdminUserFilterSpecification : FilterSpecification<User>
{
    public AdminUserFilterSpecification()
    {
        this.expression = x => x.IsAdministrator;
    }
}