using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Microsoft.AspNetCore.SignalR;

namespace Festiv.Controllers;

public class PartyController : Controller
{
    private static List<Party> Parties = new List<Party>();

    // GET /<controller>
    public IActionResult Index()
    {
        ViewBag.dummyContext = Parties;

        return View();
    }

    [HttpGet]
    public IActionResult CreateEvent()
    {
        return View();
    }

    [HttpPost]
    [Route("/Party/CreateEvent")]
    public IActionResult NewEvent(string name, string description, string location, DateTime start, DateTime end)
    {
        Parties.Add(new Party(name, description, location, start, end));
        
        return Redirect("/Party");
    }

    [HttpGet]
    public IActionResult Event()
    {
        return View();
    }

    [HttpGet]
    [Route("/Party")]
    private static List<Game> games = new List<Game>(); 

    public IActionResult Index()
    {
            // List<Game> games = new List<Game>();
            // List<
            // games.Add(new Game ("cornhole", , ));
        var model = new AddPartyViewModel
        {
            Games = games
        };
        return View(model);
    }

    public IActionResult CreateGame()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Game game)
    {
        if (ModelState.IsValid)
        {
            game.Id = games.Count + 1;
            games.Add(game);
            return RedirectToAction("/Party");
        }
        return View(game);
    }

    public IActionResult GameDetails(int id)
    {
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null)
        {
            return NotFound();
        }
            
        var viewModel = new GameViewModel
        {
            GameId = game.Id,
            GameName = game.Name,
            MinPlayers = game.MinPlayers,
            MaxPlayers = game.MaxPlayers,
            WaitingPlayers = game.WaitingPlayers
            CurrentPlayers = game.CurrentPlayers
            Teams = game.Teams
        };
        return View(viewModel)
    }

    [HttpPost]
    public IActionResult RandomizeTeams(int id, string randomizerType)
    {
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        var playersToAdd = game.WaitingPlayers.Take(game.MaxPlayers).ToList();
        game.CurrentPlayers.AddRange(playersToAdd);
        game.WaitingPlayers = game.WaitingPlayers.Skip(game.MaxPlayers).ToList();

        if (randomizerType == "split")
        {
            game.Teams = SplitIntoTwoTeams(game.CurrentPlayers);
        }
        else if (randomizerType == "pairs")
        {
            game.Teams = GroupIntoPairs(game.CurrentPlayers);
        }

        return RedirectToAction("GameDetails", new { id = game.Id });
    }

    [HttpPost]
    public IActionResult SignUp(int id, string user)
    {
        var game = games.FirstOrDefault(g => g.Id == id);
        if(game == null)
        {
            return NotFound();
        }

        game.CurrentPlayers.Add(new User { Name = user});
        return RedirectToAction("GameDetails", new { id = game.Id});
    }

    private List<List<User>> SplitIntoTwoTeams(List<User> players)
    {
        var shuffled = players.OrderBy(p => Guid.NewGuid()).ToList();
        int mid = shuffled.Count / 2;
        return new List<List<User>> { shuffled.Take(mid).ToList(), shuffled.Skip(mid).ToList() };
    }

    private List<List<User>> GroupIntoPairs(List<User> players)
    {
        var shuffled = players.OrderBy(p => Guid.NewGuid()).ToList();
        var pairs = new List<List<User>>();
        for (int i = 0; i < shuffled.Count; i += 2)
        {
            var pair = new List<User> { shuffled[i] };
            if (i + 1 < shuffled.Count)
            {
                pair.Add(shuffled[i + 1]);
            }
            pairs.Add(pair);
        }
        return pairs;
    }
}
