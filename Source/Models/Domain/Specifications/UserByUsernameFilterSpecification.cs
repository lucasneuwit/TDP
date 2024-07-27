using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class UserByUsernameFilterSpecification : FilterSpecification<User>
{
    public UserByUsernameFilterSpecification(string username)
    {
        this.expression = x => x.Username == username;
    }
}