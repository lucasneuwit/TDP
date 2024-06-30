using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TDP.Models;
using TDP.Models.Application.Services;

namespace TDP.Controllers;

public class UserController(IUserService userService) : Controller
{
    private readonly IUserService userService = userService;

    // GET

    public async Task<IActionResult> Index()
    {
        if (!await this.userService.GetAdministratorAsync())
        {
            return RedirectToAction("RegisterAdmin");
        }

        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginInfo)
    {
        var loginSucceeded = await this.userService.TryLoginAsync(new LoginInfo()
        {
            Username = loginInfo.Username,
            Password = loginInfo.Password,
        });
        if (loginSucceeded)
        {
            HttpContext.Session.SetString("userId", "some-id");
            return RedirectToAction("Index", "Home");
        }
        
        return View("Error");
    }

    public IActionResult RegisterUser()
    {
        return View("RegisterUser");
    }
    
    public IActionResult RegisterAdmin()
    {
        return View("RegisterAdmin");
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegistrationViewModel registerUser)
    {
        await this.userService.RegisterUserAsync(new RegisterUser()
        {
            Username = registerUser.Username,
            Password = registerUser.Password,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Email = registerUser.Email
        });

        return RedirectToAction("GetUsers");
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAdministrator(RegistrationViewModel registerAdmin)
    {
        await this.userService.RegisterAdministratorAsync(new RegisterUser
        {
            Username = registerAdmin.Username,
            Password = registerAdmin.Password,
            FirstName = registerAdmin.FirstName,
            LastName = registerAdmin.LastName,
            Email = registerAdmin.Email
        });

        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> GetUsers()
    {
        var users = await this.userService.GetUsers();

        return View("UsersList", users);
    }
}

public class RegisterUser
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}

public class RegistrationViewModel
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}

public class LoginViewModel
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public class LoginInfo
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}