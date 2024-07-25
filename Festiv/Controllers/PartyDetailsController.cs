using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.ViewModels;
using Festiv.Data;
using Microsoft.EntityFrameworkCore;

namespace Festiv.Controllers
{
    [Route("PartyDetails")]

    public class PartyDetailsController : Controller
    {
        private readonly FestivDbContext context;

        public PartyDetailsController (FestivDbContext dbContext)
        {
            context = dbContext;
        }
        // private static List<Game> games = new List<Game>();


        [HttpGet("{partyId:int}")]
        public IActionResult PartyDetails(int partyId)
        {
           Party? party = context.Parties
            .Include(p => p.Details)
            .Include(p => p.Games) 
            .FirstOrDefault(p => p.Id == partyId);

            if (party == null)
            {
                return NotFound();
            }

            ViewBag.PartyId = partyId;

            return View("PartyDetails", party.Details);
        }

        [HttpGet("CreateGame")]
        public IActionResult CreateGame(int partyId)
        {
            AddGameViewModel addGameViewModel = new AddGameViewModel
            {
                PartyId = partyId
            };
            return View(addGameViewModel);
        }


        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame(AddGameViewModel addGameViewModel)
        {
            if(ModelState.IsValid)
            {
                Party? party = context.Parties
                    .Include(p => p.Games)
                    .FirstOrDefault(p => p.Id == addGameViewModel.PartyId);

                if(party == null)
                {
                    return NotFound();
                }

                Game newGame = new Game
                {
                    GameName = addGameViewModel.GameName,
                    MinPlayers = addGameViewModel.MinPlayers,
                    MaxPlayers = addGameViewModel.MaxPlayers,
                    PartyId = addGameViewModel.PartyId,
                    WaitingPlayers = new List<User> (),
                    CurrentPlayers = new List<User>(),
                    Teams = new List<Team>()
                };
                party.Games.Add(newGame);
                context.Games.Add(newGame);
                await context.SaveChangesAsync();
                return RedirectToAction("PartyDetails", new { partyId = addGameViewModel.PartyId });
            }
            return View(addGameViewModel);
        }

        // [HttpPost]
        // public IActionResult Create(Game game)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         game.Id = games.Count + 1;
        //         games.Add(game);
        //         return RedirectToAction("Index");
        //     }
        //     return View(game);
        // }
        [HttpGet("{partyId}/GameDetails/{gameId}")]
        public IActionResult GameDetails(int gameId)
        {
            var game = context.Games
                .Include(g => g.WaitingPlayers)
                .Include(g => g.CurrentPlayers)
                .Include(g => g.Teams)
                .FirstOrDefault(game => game.Id == gameId);

            if (game == null)
            {
                return NotFound();
            }
            
            GameDetailsViewModel gameDetailsViewModel = new GameDetailsViewModel
            {
                GameId = game.Id,
                PartyId = game.PartyId,
                GameName = game.GameName,
                MinPlayers = game.MinPlayers,
                MaxPlayers = game.MaxPlayers,
                WaitingPlayers = game.WaitingPlayers.ToList(),
                CurrentPlayers = game.CurrentPlayers.ToList(),
                Teams = new List<Team>(),
                
            };
            return View(gameDetailsViewModel);
        }

        [HttpPost]
        public IActionResult RandomizeTeams(int id, string randomizerType)
        {
            Game? game = context.Games.Include(g => g.WaitingPlayers)
                .Include(g => g.CurrentPlayers)
                .FirstOrDefault(game => game.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var playersToAdd = game.WaitingPlayers.Take(game.MaxPlayers).ToList();
            
            foreach(var player in playersToAdd)
            {
                game.CurrentPlayers.Add(player);
            }

            game.WaitingPlayers = game.WaitingPlayers.Skip(game.MaxPlayers).ToList();

            if (randomizerType == "split")
            {
                game.Teams = SplitIntoTwoTeams(playersToAdd);
            }
            else if (randomizerType == "pairs")
            {
                game.Teams = GroupIntoPairs(playersToAdd);
            }

            context.SaveChanges();

            return RedirectToAction("GameDetails", new { id = game.Id });
        }

        [HttpPost]
        public IActionResult SignUp(int id, string firstName, string lastName)
        {
            var game = context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            game.CurrentPlayers.Add(new User { FirstName = firstName, LastName = lastName });

            context.SaveChanges();

            return RedirectToAction("GameDetails", new { id = game.Id });
        }

        private List<Team> SplitIntoTwoTeams(List<User> players)
        {
            var shuffled = players.OrderBy(p => Guid.NewGuid()).ToList();
            int mid = shuffled.Count / 2;
            return new List<Team>
             { 
                new Team { Members = shuffled.Take(mid).ToList() }, 
                new Team { Members = shuffled.Skip(mid).ToList() }
             
             };
        }

        private List<Team> GroupIntoPairs(List<User> players)
        {
            var shuffled = players.OrderBy(p => Guid.NewGuid()).ToList();
            var pairs = new List<Team>();

            for (int i = 0; i < shuffled.Count; i += 2)
            {
                var pair = new Team { Members = new List<User> {shuffled[i]} };
                if (i + 1 < shuffled.Count)
                {
                    pair.Members.Add(shuffled[i + 1]);
                }
                pairs.Add(pair);
            }
            return pairs;
        }
    }
}
