using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TDP.Models;

namespace TDP.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        if (!HttpContext.Session.TryGetValue("userId", out var userId))
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