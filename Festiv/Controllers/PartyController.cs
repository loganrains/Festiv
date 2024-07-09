using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Festiv.Controllers;

public class PartyController : Controller
{
    private static List<Party> Parties = new List<Party>();

    // GET /<controller>
    public IActionResult Index()
    {
        return View(Parties);
    }

    [HttpGet]
    public IActionResult CreateEvent()
    {
        AddPartyViewModel addPartyViewModel = new AddPartyViewModel();

        return View(addPartyViewModel);
    }

    [HttpPost]
    public IActionResult CreateEvent(AddPartyViewModel addPartyViewModel)
    {
        Party newParty = new Party
        {
            Name = addPartyViewModel.Name,
            Description = addPartyViewModel.Description,
            Location = addPartyViewModel.Description,
            Start = addPartyViewModel.Start,
            End = addPartyViewModel.End
        };
        
        Parties.Add(newParty);
        
        return Redirect("/Party");
    }

    [HttpGet]
    public IActionResult Event()
    {
        return View();
    }
}