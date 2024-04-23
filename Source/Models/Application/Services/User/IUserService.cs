using TDP.Controllers;
using TDP.Models.Domain;

namespace TDP.Models.Application.Services;

public interface IUserService
{
    Task<Domain.User> GetUser(string username);

    Task RegisterUserAsync(RegisterUser registerUser);

    Task<bool> GetAdministratorAsync();
    Task RegisterAdministratorAsync(RegisterUser registerUser);
    
    Task<bool> TryLoginAsync(LoginInfo loginInfo);
    
    Task<IEnumerable<User>> GetUsers();
}