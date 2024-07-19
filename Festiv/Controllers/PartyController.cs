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

namespace Festiv.Controllers
{
    public class PartyController : Controller
    {   
        private FestivDbContext context;

        public PartyController (FestivDbContext dbContext)
        {
            context = dbContext;
        }

        private static List<Party> Parties = [];
        private static List<Game> games = [];

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
        public async Task<IActionResult> CreateEvent(AddPartyViewModel addPartyViewModel)
        {
            if(ModelState.IsValid)
            {
                 Party newParty = new Party
                {
                    Name = addPartyViewModel.Name,
                };

                PartyDetails theDetails = new PartyDetails
                {
                    Name = addPartyViewModel.Name,
                    Description = addPartyViewModel.Description,
                    Location = addPartyViewModel.Location,
                    Start = addPartyViewModel.Start,
                    End = addPartyViewModel.End
                };

                newParty.Details = theDetails;
                theDetails.Party = newParty;

                context.Parties.Add(newParty);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");



            //Code is unreachable can we remove? - Log
            context.Parties.Add(newParty);
            context.SaveChanges();
            return RedirectToAction("Index", "Party");
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

        [HttpGet("Party/PartyDetails/{partyid}/AddPhoto")]
        public IActionResult AddPhoto(int partyid, string url, string alt)
        {
            return View(PartyDetails);
        }
    }
}