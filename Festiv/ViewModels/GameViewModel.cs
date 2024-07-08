using System.ComponentModel.DataAnnotations;
using Festiv.Model.User;

namespace Festiv.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Game name required")]
        public string Name { get; set;}
        [Required(ErrorMessage = "Need Players for Game")]
        public List<User> CurrentPlayers { get; set; } 
        public List<List<User>> Teams { get; set; }

        public GameViewModel()
        {
            CurrentPlayers = new List<User>();
            Teams = new List<List<User>>();
        }
    }
}