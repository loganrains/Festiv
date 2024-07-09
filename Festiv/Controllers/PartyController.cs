using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        if(ModelState.IsValid)
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

        return View(addPartyViewModel);

    }

    [HttpGet]
    public IActionResult Event()
    {
        return View();
    }
}