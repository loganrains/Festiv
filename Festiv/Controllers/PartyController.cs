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
    private FestivDbContext context;

    public PartyController (FestivDbContext dbContext)
    {
        context = dbContext;
    }

    // GET /<controller>
    public IActionResult Index()
    {
        List<Party> Parties = context.Parties.ToList();
        
        return View(Parties);
    }

    [HttpGet("Party/PartyDetails/{partyId}")]
    public IActionResult PartyDetails(int partyId)
    {
        PartyDetails? requestedParty = context.PartyDetails.Find(partyId);

        if (requestedParty != null)
        {
            ViewBag.Name = context.PartyDetails.Find(partyId);
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
                Name = addPartyViewModel.Name,
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
            
            context.Parties.Add(newParty);
            context.SaveChanges();
            
            return Redirect("/Party");
        }

        return View(addPartyViewModel);

    }

        [HttpGet("Party/Delete/{partyId}")]
        public IActionResult Delete(int partyId)
        {
            var partyToDelete = context.Parties.Include(p => p.Details).FirstOrDefault(x => x.Id == partyId);
            if (partyToDelete == null)
            {
                return NotFound();
            }

            return View(partyToDelete);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var partyToDelete = context.Parties.Include(p => p.Details).FirstOrDefault(x => x.Id == id);
            if (partyToDelete != null)
            {
                context.Parties.Remove(partyToDelete);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
}