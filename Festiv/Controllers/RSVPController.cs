using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using System.Collections.Generic;

namespace Festiv.Controllers
{
    public class RsvpController : Controller
    {
        private static List<GuestRespond> GuestResponses = new List<GuestRespond>();

        // GET: /Rsvp/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Rsvp/Add
        [HttpPost]
        public IActionResult Add(string firstName, string lastName, string email)
        {
            var guestRespond = new GuestRespond(firstName, lastName, email);

            if (ModelState.IsValid)
            {
                // Add guest to the list
                GuestResponses.Add(guestRespond);

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
            return View(GuestResponses);
        }
    }
}
