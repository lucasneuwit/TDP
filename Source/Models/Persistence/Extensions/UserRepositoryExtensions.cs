using TDP.Models.Domain;

namespace TDP.Models.Persistence;

/// <summary>
/// Defines extension methods over <see cref="IRepository{TEntity}"/>.
/// </summary>
public static class UserRepositoryExtensions
{
    /// <summary>
    /// Verifies if an <see cref="Administrator"/> instance exists.
    /// </summary>
    /// <param name="repository">The <see cref="IRepository{TEntity}"/> instance.</param>
    /// <returns>A value indicating whether a <see cref="Administrator"/> instance was found.</returns>
    public static Task<bool> AdminExistsAsync(this IRepository<User> repository)
    {
        var specification = new AdminUserFilterSpecification();
        return repository.AnyAsync(specification);
    }

    /// <summary>
    /// Finds a user by its username.
    /// </summary>
    /// <param name="repository">The <see cref="IRepository{TEntity}"/> instance.</param>
    /// <param name="username">The username.</param>
    /// <returns>The <see cref="User"/> instance, if found.</returns>
    public static Task<User?> FindByUsernameAsync(this IRepository<User> repository, string username)
    {
        var specification = new UserByUsernameFilterSpecification(username);
        return repository.FirstOrDefaultAsync(specification);
    }
}