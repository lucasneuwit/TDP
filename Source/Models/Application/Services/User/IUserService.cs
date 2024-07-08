using TDP.Controllers;
using TDP.Models.Domain;

namespace TDP.Models.Application.Services;

public interface IUserService
{
    Task DeleteUserAsync(Guid userId);

    Task UpdateUserAsync(UpdateUser updateUser);
    
    Task<Domain.User> GetUserAsync(string username);

    Task<Domain.User> GetUserAsync(Guid userId);

    Task RegisterUserAsync(RegisterUser registerUser);

    Task<bool> GetAdministratorAsync();
    Task RegisterAdministratorAsync(RegisterUser registerUser);
    
    Task<Guid?> TryLoginAsync(LoginInfo loginInfo);
    
    Task<IEnumerable<User>> GetUsers();
}