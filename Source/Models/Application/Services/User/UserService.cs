using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;
using TDP.Models.Persistence;

namespace TDP.Models.Application;

public class UserService : IUserService
{
    private readonly IRepository<User> repository;
    private readonly ILogger<IUserService> logger;
        
    public UserService(IRepository<User> repository, ILogger<IUserService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public Task DeleteUserAsync(Guid userId)
    {
        this.logger.LogInformation("Removing user data with id: {id}", userId);
        return this.repository.DeleteAsync(userId);
    }

    public async Task UpdateUserAsync(UpdateUser updateUser)
    {
        var user = await this.repository.FindByIdOrThrowAsync(updateUser.Id);
        user.SetUsername(updateUser.Username);
        user.SetName(updateUser.FirstName);
        user.SetLastname(updateUser.LastName);
        user.SetEmailAddress(updateUser.EmailAddress);
        if (updateUser.ProfilePicture is not null)
        {
            user.SetProfilePicture(updateUser.ProfilePicture);
        }

        await this.repository.UpdateAsync(user);
    }

    public Task<User> GetUserAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(Guid userId)
    {
        return this.repository.FindByIdOrThrowAsync(userId);
    }

    public Task RegisterUserAsync(RegisterUser registerUser)
    {
        this.logger.LogInformation("Attempting to register new user with name: {username}", registerUser.Username);
        var user = new User(Guid.NewGuid());
        UpdateUser(user, registerUser);

        return repository.CreateAsync(user);
    }

    public Task<bool> GetAdministratorAsync()
    {
        return this.repository.AdminExistsAsync();
    }

    public async Task RegisterAdministratorAsync(RegisterUser registerUser)
    {
        this.logger.LogInformation("Attempting to register new user with name: {username} with administration privileges", registerUser.Username);
        var administrator = new Administrator(Guid.NewGuid());
        UpdateUser(administrator, registerUser);

        await repository.CreateAsync(administrator);
    }

    public async Task<Guid?> TryLoginAsync(LoginInfo loginInfo)
    {
        this.logger.LogInformation("New login for user: {username}", loginInfo.Username);
        var user = await this.repository.FindByUsernameAsync(loginInfo.Username);
        if (user is not null && loginInfo.Password == user.PasswordHash)
        {
            return user.Id;
        }

        return null;
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        return this.repository.AllAsync();
    }

    private static void UpdateUser(User user, RegisterUser userData)
    {
        user.SetName(userData.FirstName);
        user.SetLastname(userData.LastName);
        user.SetEmailAddress(userData.Email);
        user.SetUsername(userData.Username);
        user.SetPassword(userData.Password);
        user.SetBirthday(DateOnly.FromDateTime(DateTime.Now));
    }
}