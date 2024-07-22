using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.Data; // Ensure to include the namespace for FestivDbContext
using System.Collections.Generic;
using Microsoft.Extensions.Logging;


namespace Festiv.Controllers
{
    public class RsvpController : Controller
    {
        private readonly FestivDbContext _context;

        public RsvpController(FestivDbContext context)
        {
            _context = context;
        }

        // GET: /Rsvp/Index
        public IActionResult Index()
        {
            List<Party> parties = _context.Parties.ToList();
            ViewBag.parties = parties;
            return View();
        }

        // POST: /Rsvp/Add
        [HttpPost]
        public IActionResult Add(GuestRespond guestRespond)
        {
            if (ModelState.IsValid)
            {
                // Save guest response to the database
                _context.GuestResponds.Add(guestRespond);
                _context.SaveChanges();

                // Redirect to guest list action
                return RedirectToAction("GuestList");
            }

            // If model state is not valid, return to the same view with validation errors
            return View(guestRespond);
        }

      
        // GET: /Rsvp/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }

        // GET: /Rsvp/GuestList (Display list of guests)
        public IActionResult GuestList()
        {
            var guestResponses = _context.GuestResponds.ToList();
            return View(guestResponses);
        }
    }
}
