using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Festiv.Models;

namespace Festiv.ViewModels
{
    public class GameDetailsViewModel
    {
        public int GameId { get; set; }
        [Required(ErrorMessage = "Game name required")]
        public string ? GameName { get; set;}
        [Required(ErrorMessage = "Must set player minimum")]
        public int MinPlayers { get; set; }
        [Required(ErrorMessage = "Must set player maximum")]
        public int MaxPlayers { get; set; }
        [Required(ErrorMessage = "Need players for game")]
        public List<User>? WaitingPlayers { get; set; }
        public List<User>? CurrentPlayers { get; set; } 
        public List<List<User>>? Teams { get; set; }
    }
}