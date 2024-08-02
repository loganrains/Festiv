using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using Festiv.ViewModels;
using Festiv.Data;
using Festiv.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Festiv.Controllers
{
    [Route("PartyDetails")]
    public class PartyDetailsController : Controller
    {
        private readonly FestivDbContext _context;
        private readonly SpotifyService _spotifyService;

        public PartyDetailsController(FestivDbContext context, SpotifyService spotifyService)
        {
            _context = context;
            _spotifyService = spotifyService;
        }

        [HttpGet("{partyId}")]
        public async Task<IActionResult> PartyDetails(int partyId)
        {
            Console.WriteLine($"Got here with PartyDetails controller {partyId}");
            // Fetch party and related details
            var party = await _context.Parties
                .Include(p => p.Details)
                .Include(p => p.Games)
                .FirstOrDefaultAsync(p => p.Id == partyId);

            if (party == null)
            {
                return NotFound();
            }

            // Get access token from the session
            var accessToken = HttpContext.Session.GetString("SpotifyAccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Spotify");
            }

            // Fetch current track details from Spotify
            var trackId = "5rb9QrpfcKFHM1EUbSIurX?si=e048ed81e62345d7";
            var currentTrack = await _spotifyService.GetTrackAsync(trackId, accessToken);
            var playlistId = "37i9dQZF1DWXti3N4Wp5xy?si=732ec57f58c4404a";
            // Prepare view model with party details, games, and current track
            Console.WriteLine($"Done Login {partyId}");
            var partyDetailsViewModel = new PartyDetailsViewModel
            {
                Party = party,
                Games = party.Games.ToList(),
                CurrentTrack = currentTrack,
                PlaylistId = playlistId 
            };

            return View(partyDetailsViewModel);
        }

        [HttpGet("{partyId}/CreateGame")]
        public IActionResult CreateGame(int partyId)
        {
            var addGameViewModel = new AddGameViewModel
            {
                PartyId = partyId
            };
            return View(addGameViewModel);
        }

        [HttpPost("{partyId}/CreateGame")]
        public async Task<IActionResult> CreateGame(AddGameViewModel addGameViewModel)
        {
            if (ModelState.IsValid)
            {
                var party = await _context.Parties
                    .Include(p => p.Games)
                    .FirstOrDefaultAsync(p => p.Id == addGameViewModel.PartyId);

                if (party == null)
                {
                    return NotFound();
                }

                var newGame = new Game
                {
                    GameName = addGameViewModel.GameName,
                    MinPlayers = addGameViewModel.MinPlayers,
                    MaxPlayers = addGameViewModel.MaxPlayers,
                    PartyId = addGameViewModel.PartyId,
                    WaitingPlayers = new List<User>(),
                    CurrentPlayers = new List<User>(),
                    Teams = new List<Team>()
                };
                party.Games.Add(newGame);
                _context.Games.Add(newGame);
                await _context.SaveChangesAsync();
                return RedirectToAction("PartyDetails", new { partyId = addGameViewModel.PartyId });
            }
            return View(addGameViewModel);
        }

        [HttpGet("{partyId}/GameDetails/{gameId}")]
        public IActionResult GameDetails(int partyId, int gameId)
        {
            var game = _context.Games
                .Include(g => g.WaitingPlayers)
                .Include(g => g.CurrentPlayers)
                .Include(g => g.Teams)
                .ThenInclude(t => t.Members)
                .FirstOrDefault(g => g.GameId == gameId);

            if (game == null)
            {
                return NotFound();
            }

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

            return View(gameDetailsViewModel);
        }

        [HttpPost("{partyId}/GameDetails/{gameId}/SignUp")]
        public async Task<IActionResult> SignUp(int partyId, int gameId, string firstName, string lastName)
        {
            var game = await _context.Games
                .Include(g => g.WaitingPlayers)
                .FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                return NotFound();
            }

            var user = new User { FirstName = firstName, LastName = lastName };
            game.WaitingPlayers.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("GameDetails", new { partyId = partyId, gameId = gameId });
        }

        [HttpPost("{partyId}/GameDetails/{gameId}/RandomizeTeams")]
        public async Task<IActionResult> RandomizeTeams(int partyId, int gameId, string randomizerType)
        {
            var game = await _context.Games
                .Include(g => g.WaitingPlayers)
                .Include(g => g.CurrentPlayers)
                .Include(g => g.Teams)
                .FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                return NotFound();
            }

            game.Teams.Clear();

            if (!game.CurrentPlayers.Any())
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

            _context.Games.Update(game);
            await _context.SaveChangesAsync();

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
                var pair = new Team { Members = new List<User> { shuffled[i] } };
                if (i + 1 < shuffled.Count)
                {
                    pair.Members.Add(shuffled[i + 1]);
                }
                pairs.Add(pair);
            }
            return pairs;
        }


        [HttpPost]
        public async Task<IActionResult> AddTrackToPlaylist(string trackUri, string playlistId)
        {
            var accessToken = HttpContext.Session.GetString("SpotifyAccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Spotify access token is missing.");
            }
            await _spotifyService.AddTrackToPlaylist(accessToken, trackUri);
            return RedirectToAction("PartyDetails", new { partyId = ViewBag.PartyId });
        }
    }
}
