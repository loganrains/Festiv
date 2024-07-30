using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.ViewModels;
using Festiv.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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

        [HttpGet("{partyId}")]
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

            var partyDetails = new PartyDetailsViewModel
            {
                Party = party,
                Games = party.Games.ToList()
            };

            ViewBag.PartyId = partyId;
            return View(partyDetails);
        }

        [HttpGet("{partyId}/CreateGame")]
        public IActionResult CreateGame(int partyId)
        {
            AddGameViewModel addGameViewModel = new AddGameViewModel
            {
                PartyId = partyId
            };
            return View(addGameViewModel);
        }


        [HttpPost("{partyId}/CreateGame")]
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

        [HttpGet("{partyId}/GameDetails/{gameId}")]
        public IActionResult GameDetails(int partyId, int gameId)
        {
            var game = context.Games
                .Include(g => g.WaitingPlayers)
                .Include(g => g.CurrentPlayers)
                .Include(g => g.Teams)
                .ThenInclude(t => t.Members)
                .FirstOrDefault(game => game.GameId == gameId);

            if (game == null)
            {
                return NotFound();
            }
            
            GameDetailsViewModel gameDetailsViewModel = new GameDetailsViewModel
            {
                GameId = game.GameId,
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

        [HttpPost("{partyId}/GameDetails/{gameId}/SignUp")]
public async Task<IActionResult> SignUp(int partyId, int gameId, string firstName, string lastName)
{
    var game = context.Games
        .Include(g => g.WaitingPlayers)
        .FirstOrDefault(g => g.GameId == gameId);

    if (game == null)
    {
        return NotFound();
    }

    var user = new User { FirstName = firstName, LastName = lastName };
    game.WaitingPlayers.Add(user);
    await context.SaveChangesAsync();

    return RedirectToAction("GameDetails", new { partyId = partyId, gameId = gameId });
}


        [HttpPost("{partyId}/GameDetails/{gameId}/RandomizeTeams")]
        public async Task<IActionResult> RandomizeTeams(int partyId, int gameId, string randomizerType)
        {
            Game? game = context.Games
                .Include(g => g.WaitingPlayers)
                .Include(g => g.CurrentPlayers)
                // .Include(t => t.Members)
                .Include(g => g.Teams)
                .FirstOrDefault(game => game.GameId == gameId);

            if (game == null)
            {
                return NotFound();
            }

            game.Teams.Clear();

            if(!game.CurrentPlayers.Any())
            {
                game.CurrentPlayers = game.WaitingPlayers.ToList();
                game.WaitingPlayers.Clear();
            }

            List<User> playerToShuffle = game.CurrentPlayers.ToList();

            if (randomizerType == "split")
            {
                game.Teams = SplitIntoTwoTeams(playerToShuffle);
            }
            else if (randomizerType == "pairs")
            {
                game.Teams = GroupIntoPairs(playerToShuffle);
            }

            context.Games.Update(game);
            await context.SaveChangesAsync();
            
             var gameDetailsViewModel = new GameDetailsViewModel
            {
                GameId = game.GameId,
                PartyId = game.PartyId,
                GameName = game.GameName,
                MinPlayers = game.MinPlayers,
                MaxPlayers = game.MaxPlayers,
                WaitingPlayers = game.WaitingPlayers.ToList(),
                CurrentPlayers = game.CurrentPlayers.ToList(),
                Teams = game.Teams.ToList()
            };

            return View("GameDetails", gameDetailsViewModel);
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