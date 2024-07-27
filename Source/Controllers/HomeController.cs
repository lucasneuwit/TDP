using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TDP.Models;
using TDP.Models.Application;
using TDP.Models.ViewModels;
using TDP.Utils;

namespace TDP.Controllers;

public class HomeController : Controller
{
    private readonly IUserService userService;
    
    public HomeController(IUserService userService)
    {
        this.ViewData["profilePicture"] = AppConstants.DefaultProfilePicture;
        this.userService = userService;
    }

    public IActionResult Index()
    {
        if (!HttpContext.Session.TryGetValue("user-id", out var userId))
        {
            ViewBag.Userid = userId!;
            return RedirectToAction("Index", "User");
        }
        
        return View();
    }

    public IActionResult Watchlist()
    {
        // ReSharper disable once Mvc.ViewNotResolved
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}