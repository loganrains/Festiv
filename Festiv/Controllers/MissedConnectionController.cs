// Controllers/MissedConnectionsController.cs
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class MissedConnectionsController : Controller
{
    private readonly FestivDbContext _context;

    public MissedConnectionsController(FestivDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(MissedConnection model)
    {
        if (ModelState.IsValid)
        {
            _context.MissedConnections.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("PartyDetails", "PartyDetails", new { partyId = 1 }); // Adjust partyId as needed
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.MissedConnections.FindAsync(id);
        if (post != null)
        {
            _context.MissedConnections.Remove(post);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("PartyDetails", "PartyDetails", new { partyId = 1 }); // Adjust partyId as needed
    }
}
