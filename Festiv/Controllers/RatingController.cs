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
    
    [HttpPost]
    public IActionResult UpdateGuestRating(int guest, bool confirmedGuestShowedUp, bool guestBroughtGift)
    {
        GuestList.Add(dummyUser);
        if(confirmedGuestShowedUp)
        {
            GuestList[guest].Rating += 50;
        }
        if(guestBroughtGift)
        {
            GuestList[guest].Rating += 100;
        }
        return Redirect("/Party");
    }
}