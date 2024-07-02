using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;

namespace Festiv.Controllers;

public class PartyController : Controller
{

    // GET /<controller>
    public IActionResult Index()
    {
        List<Party> Parties = new List<Party>();

        Party birthday = new Party("birthday", "party", "House");
        Parties.Add(birthday);
        ViewBag.dummyContext = Parties;

        return View();
    }
}