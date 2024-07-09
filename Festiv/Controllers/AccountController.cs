using Microsoft.AspNetCore.Mvc;

namespace Festiv.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

        public IActionResult Register()
    {
        return View();
    }

        public IActionResult Logout()
    {
        return View();
    }

}
