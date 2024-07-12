using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Microsoft.AspNetCore.SignalR;
using System.Configuration;
using Festiv.Data;

namespace Festiv.Controllers;

public class RatingController : Controller
{
    private readonly FestivDbContext context;
    private List<User> GuestList = [];
    private User dummyUser = new User(false, "dummy","mcDumbFace", "no pic");

    public RatingController(FestivDbContext dbContext)
    {
        context = dbContext;
    }

    public IActionResult Index()
    {
        List<User> users = context.Users
            .ToList();

        return View(users);
    }
    // [HttpPost]
    // [Route("/UpdateGuestRating")]
    // public IActionResult Index()
    // {

    // }
    
    [HttpPost]
    public IActionResult Update(int UserId, bool confirmedGuestShowedUp, bool guestBroughtGift)
    {
        User theUser = context.Users.Find(UserId);
        if(confirmedGuestShowedUp)
        {
            theUser.Rating += 50;
        }
        if(guestBroughtGift)
        {
            theUser.Rating += 100;
        }
        return Redirect("/Party");
    }
}