using TDP.Models.Domain;
using TDP.Models.Domain.Specification;
using TDP.Models.Domain.Specifications;

namespace TDP.Models.Persistence.Extensions;

public static class UserRepositoryExtensions
{
    public static Task<bool> AdminExistsAsync(this IRepository<User> repository)
    {
        var specification = new AdminUserFilterSpecification();
        return repository.AnyAsync(specification);
    }

    public static Task<User?> FindByUsernameAsync(this IRepository<User> repository, string username)
    {
        var specification = new UserByUsernameFilterSpecification(username);
        return repository.FirstOrDefaultAsync(specification);
    }

    public class UserByUsernameFilterSpecification : FilterSpecification<User>
    {
        public UserByUsernameFilterSpecification(string username)
        {
            this.expression = x => x.Username == username;
        }
    }
}