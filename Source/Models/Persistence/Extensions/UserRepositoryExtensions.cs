using TDP.Models.Domain;

namespace TDP.Models.Persistence;

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
    public static Task<User?> FindByUserIdAsync(this IRepository<User> repository, Guid userId)
    {
        var specification = new UserByIdFilterSpecification(userId);
        return repository.FirstOrDefaultAsync(specification);
    }

    public class UserByIdFilterSpecification : FilterSpecification<User>
    {
        public UserByIdFilterSpecification(Guid userId)
        {
            this.expression = x => x.Id.Equals(userId);
        }
    }
}