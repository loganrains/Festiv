using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Festiv.Models;

namespace Festiv.ViewModels
{
    public class AddGameViewModel
    {
        public int PartyId {get; set; }
        public string? GameName { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
    }
}