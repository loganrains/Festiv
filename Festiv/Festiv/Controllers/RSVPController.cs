
using Microsoft.AspNetCore.Mvc;
using PartyRsvp.Models;

namespace Festiv.Controllers;
public class RsvpController : Controller
{

public ActionResult Index()
{
    
    return View();
}

[HttpPost]
public ActionResult Index(GuestRespond guestRespond)
{
    if (ModelState.IsValid)
    {
        
        
    }
    return View(guestRespond);

}
}