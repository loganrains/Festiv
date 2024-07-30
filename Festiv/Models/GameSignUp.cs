using Microsoft.AspNetCore.Identity;

namespace Festiv.Models
{
    public class GameSignUp
    {
        public int GameSignUpId { get; set; }
        public int GameId { get; set; }
        public int PartyId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}