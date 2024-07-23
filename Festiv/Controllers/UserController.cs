using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Microsoft.AspNetCore.Identity;
using Festiv.Data;
using Festiv.Areas.Identity.Pages.Account;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Abstractions;
using Azure.Identity;
using Microsoft.Build.Framework.Profiler;
using Microsoft.AspNetCore.Authorization;

namespace Festiv.Controllers;

public class UserController : Controller
{
    private readonly FestivDbContext context;
    private readonly UserManager<User> userManager;
    public UserController(FestivDbContext dbContext, UserManager<User> userManager)
    {
        context = dbContext;
        this.userManager = userManager;
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
    public async Task<IActionResult> Profile(string id)
    {
        var userToView = context.Users.SingleOrDefault(x => x.Id.ToString() == id);
        if (userToView == null)
            {
                return NotFound();
            }

        var currentUser = await userManager.GetUserAsync(User);
        var isAdmin = await userManager.IsInRoleAsync(currentUser, "Admin");

        ViewData["IsAdmin"] = isAdmin;

        return View(userToView);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View("Error");
    }
}

        // string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        // User currentUser = context.Users.Where(x => x.Id.ToString() == userId).SingleOrDefault();
