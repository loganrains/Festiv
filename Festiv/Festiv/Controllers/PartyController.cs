using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Microsoft.AspNetCore.SignalR;

namespace Festiv.Controllers;

public class PartyController : Controller
{
    private static List<Party> Parties = new List<Party>();

    // GET /<controller>
    public IActionResult Index()
    {
        ViewBag.dummyContext = Parties;

        return View();
    }

    [HttpGet]
    public IActionResult CreateEvent()
    {
        return View();
    }

    [HttpPost]
    [Route("/Party/CreateEvent")]
    public IActionResult NewEvent(Party NewEvent)
    {
        Parties.Add(NewEvent);
        
        return Redirect("/Party");
    }
}