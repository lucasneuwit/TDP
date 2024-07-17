using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TDP.Extensions;
using TDP.Models;
using TDP.Models.Application;
using TDP.Models.Application.DataTransfer;
using TDP.Models.ViewModels;

namespace TDP.Controllers;

public class UserController(IUserService userService) : Controller
{

    // GET
    public async Task<IActionResult> Index()
    {
        if (!await userService.GetAdministratorAsync())
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
        var userId = await userService.TryLoginAsync(new LoginInfo()
        {
            Username = loginInfo.Username,
            Password = loginInfo.Password,
        });
        if (userId is not null)
        {
            HttpContext.Session.SetString("user-id", userId.ToString());
            var currentUser = await userService.GetUserAsync(userId.ToGuid());
            if (currentUser.ProfilePicture is not null)
            {
                ViewData["profilePicture"] =
                    $"data:image/*;base64,{Convert.ToBase64String(currentUser.ProfilePicture)}";
            }
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
        await userService.RegisterUserAsync(new RegisterUser()
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
    public async Task<IActionResult> RemoveUser(Guid userId)
    {
        await userService.DeleteUserAsync(userId);

        return RedirectToAction("GetUsers");
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAdministrator(RegistrationViewModel registerAdmin)
    {
        await userService.RegisterAdministratorAsync(new RegisterUser
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
        var currentUserId = HttpContext.GetCurrentUserId();
        var users = (await userService.GetUsers()).Select(e => new UserViewModel(e)).ToList();

        return View("UsersList", new UsersListViewModel(currentUserId, users));
    }

    [HttpPost]
    public async Task<IActionResult> Profile(UserViewModel userViewModel)
    {
        byte[]? profilePicture = null;
        if (userViewModel.NewProfilePicture is not null)
        {
            using var stream = new MemoryStream();
            await userViewModel.NewProfilePicture.CopyToAsync(stream);
            profilePicture = stream.ToArray();
        }
        var updateUser = new UpdateUser(
            userViewModel.Id,
            userViewModel.Username,
            userViewModel.FirstName,
            userViewModel.LastName,
            userViewModel.EmailAddress,
            profilePicture);

        await userService.UpdateUserAsync(updateUser);

        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> Profile()
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        var currentUser = await userService.GetUserAsync(currentUserId);

        return View("UserProfile", new UserViewModel(currentUser));
    }
}