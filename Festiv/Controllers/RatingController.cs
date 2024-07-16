using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Microsoft.AspNetCore.SignalR;
using System.Configuration;
using Festiv.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Festiv.ViewModels;
using Microsoft.Extensions.Configuration.UserSecrets;

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
        List<User> users = context.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public IActionResult Update(IFormCollection user)
    {
        string? UserId = user["guest"];
        User? guestToUpdate = context.Users.Where(x => x.Id.ToString() == UserId).SingleOrDefault();
        return View(guestToUpdate);
    }

    [HttpPost]
    public IActionResult ChangeRating(IFormCollection form)
    {
        string? confirmedGuestShowedUp = form["confirmedGuestShowedUp"];
        string? guestBroughtGift = form["guestBroughtGift"];
        string? UserId = form["guest"];
        var UserToUpdate = context.Users.Where(x => x.Id.ToString() == UserId).SingleOrDefault();
 
        if(confirmedGuestShowedUp == "on")
        {
            UserToUpdate.Rating += 50;
        } else 
        {
            UserToUpdate.Rating -= 50;
        }
        if(guestBroughtGift == "on")
        {
            UserToUpdate.Rating += 100;
        } else 
        {
            UserToUpdate.Rating -= 100;
        }
        context.SaveChanges();
        return Redirect("../Party");
    }
}

























    // public IActionResult Index()
    // {
    //     List<User> users = context.Users
    //         .ToList();

    //     return View(users);
    // }
    // [HttpPost]
    // [Route("/Update")]
    // public IActionResult Index(string userId)
    // {  
    //     User userToUpdate = context.Users.Find(userId);
    //     ViewBag.userToUpdate = userToUpdate;
    //     return Redirect("/Update");
    // }
    // public IActionResult Update()
    // {
    //     User user = ViewBag.userToUpdate;
 
    //     return View(user);
    // }

    // [HttpPost]
    // public IActionResult Update(bool confirmedGuestShowedUp, bool guestBroughtGift)
    // {
    //     string userId = ViewBag.userIdToUpdate;
    //     if(confirmedGuestShowedUp)
    //     {
    //         context.Users.Find(userId).Rating += 50;
    //     }
    //     if(guestBroughtGift)
    //     {
    //         context.Users.Find(userId).Rating += 100;
    //     }
    //     context.SaveChanges();
    //     return Redirect("/Party");
    // }