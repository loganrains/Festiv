using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.ViewModels;
using Festiv.Data;
using Microsoft.AspNetCore.SignalR;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.EntityFrameworkCore;

namespace Festiv.Controllers;

public class PartyController : Controller
{
    private static List<Party> Parties = new List<Party>();
    

    // GET /<controller>
    public IActionResult Index()
    {
        return View(Parties);
    }

    [HttpGet("Party/PartyDetails/{partyId}")]
    public IActionResult PartyDetails(int partyId)
    {
        Party? requestedParty = Parties.Find(x => x.Id.Equals(partyId));

        if (requestedParty != null)
        {
            return View("PartyDetails", requestedParty);
        }

        return View();
        
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
            PartyDetails theDetails = new()
            {
                Description = addPartyViewModel.Description,
                Location = addPartyViewModel.Location,
                Start = addPartyViewModel.Start,
                End = addPartyViewModel.End
            };
            Party newParty = new()
            {
                Name = addPartyViewModel.Name,
                Details = theDetails
            };
            
            Parties.Add(newParty);
            
            return Redirect("/Party");
        }

        return View(addPartyViewModel);

    }
}