using TDP.Controllers;
using TDP.Models.Domain;
using TDP.Models.Persistence.Extensions;
using TDP.Models.Persistence.UnitOfWork;

namespace TDP.Models.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> repository;
    private readonly ILogger<IUserService> logger;
        
    public UserService(IRepository<User> repository, IUnitOfWorkManager uowManager, ILogger<IUserService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public Task<User> GetUser(string username)
    {
        throw new NotImplementedException();
    }

    public Task RegisterUserAsync(RegisterUser registerUser)
    {
        this.logger.LogInformation("Attempting to register new user with username: {username}", registerUser.Username);
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
        var administrator = new Administrator(Guid.NewGuid());
        UpdateUser(administrator, registerUser);

        await repository.CreateAsync(administrator);
    }

    public async Task<bool> TryLoginAsync(LoginInfo loginInfo)
    {
        var user = await this.repository.FindByUsernameAsync(loginInfo.Username);
        if (user is not null)
        {
            return loginInfo.Password == user.PasswordHash;
        }

        return false;
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