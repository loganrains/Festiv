using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using System.Collections.Generic;
using System.Linq;

namespace Festiv.Controllers
{
    public class GameBoardController : Controller
    {
        private static List<Game> games = new List<Game>(); 

        public IActionResult Index()
        {
            return View(games);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                games.Add(game);
                return RedirectToAction("Index");
            }
            return View(game);
        }

        public IActionResult Details(int id)
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost]
        public IActionResult RandomizeTeams(int id, string randomizerType)
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            if (randomizerType == "split")
            {
                game.Teams = SplitIntoTwoTeams(game.CurrentPlayers);
            }
            else if (randomizerType == "pairs")
            {
                game.Teams = GroupIntoPairs(game.CurrentPlayers);
            }

            return RedirectToAction("Details", new { id = game.Id });
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
}
