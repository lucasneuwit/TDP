namespace TDP.Models.Application.Services;

public interface IUserService
{
    Task<Domain.User> GetUser(string username);
}