using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;

namespace TDP.Models.Application;

public interface IUserService
{
    /// <summary>
    /// Attempts to delete a user from the application storage.
    /// </summary>
    /// <param name="userId">The id of the user to be removed.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteUserAsync(Guid userId);

    /// <summary>
    /// Updates data for a user.
    /// </summary>
    /// <param name="updateUser">A DTO holding the updated user data.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateUserAsync(UpdateUser updateUser);

    /// <summary>
    /// Finds a <see cref="User"/> by its id.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A <see cref="User"/>, if found.</returns>
    Task<Domain.User> GetUserAsync(Guid userId);

    /// <summary>
    /// Creates a new <see cref="User"/> with the provided information.
    /// </summary>
    /// <param name="registerUser">The user registration data.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task RegisterUserAsync(RegisterUser registerUser);

    /// <summary>
    /// Evaluates if an <see cref="Administrator"/> user exists in the application storage.
    /// </summary>
    /// <returns>A value indicating whether a <see cref="Administrator"/> instance was found.</returns>
    Task<bool> AdministratorExistsAsync();
    
    /// <summary>
    /// Creates a new <see cref="Administrator"/> with the provided information.
    /// </summary>
    /// <param name="registerUser">The user registration data.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task RegisterAdministratorAsync(RegisterUser registerUser);
    
    /// <summary>
    /// Attempts to log in with the provided credentials.
    /// </summary>
    /// <param name="loginInfo">The login credentials to be used</param>
    /// <returns>The user id, if the login was successful. Null otherwise.</returns>
    Task<Guid?> TryLoginAsync(LoginInfo loginInfo);
    
    /// <summary>
    /// Gets all users in the application storage.
    /// </summary>
    /// <returns>A list of <see cref="User"/>.</returns>
    Task<IEnumerable<User>> GetUsers();
}