using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Microsoft.AspNetCore.Identity;
using Festiv.Data;
using Festiv.Areas.Identity.Pages.Account;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Abstractions;
using Azure.Identity;
using Microsoft.Build.Framework.Profiler;

namespace Festiv.Controllers;

public class UserController : Controller
{
    // private readonly User _user;
    private readonly FestivDbContext context;
    // public readonly IHttpContextAccessor? _httpContextAccessor;

    public UserController(FestivDbContext dbContext)
    {
        context = dbContext;
    }
    public IActionResult Index()
    {
        List<User> users = context.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public IActionResult GetProfile(IFormCollection form)
    {
        string UserId = form["guest"];
        return RedirectToAction("Profile", new {id = UserId});
    }

    [HttpGet("/User/Profile/{id}")]
    public IActionResult Profile(string id)
    {
        User UserToView = context.Users.Where(x => x.Id.ToString() == id).SingleOrDefault();
        return View(UserToView);
    }
}

        // string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        // User currentUser = context.Users.Where(x => x.Id.ToString() == userId).SingleOrDefault();
