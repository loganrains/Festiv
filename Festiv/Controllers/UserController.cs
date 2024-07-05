using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Microsoft.AspNetCore.Identity;

namespace Festiv.Controllers;

public class UserController : Controller
{
    private readonly Festiv.Models.User _user;

    public UserController(Festiv.Models.User user)
    {
        _user = user;
    }

    public IActionResult Index()
    {
        return View();
    }
}
