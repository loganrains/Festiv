using System;
using System.Collections.Generic;
namespace Festiv.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? GameName { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public ICollection<User> WaitingPlayers { get; set; } = new List<User>();
        public ICollection<User> CurrentPlayers { get; set; } = new List<User>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public Game()
        {
        }
        public Game(string gameName, List<User> waitingPlayers, List<User> currentPlayers, List<List<User>> teams, int minPlayers, int maxplayers)
        {
            GameName = gameName;
            MinPlayers = minPlayers;
            MaxPlayers = maxplayers;
        }
        

        public override string ToString()
        {
            return GameName;
        }

        public override bool Equals(object? obj)
        {
            return obj is Game game && 
                Id == game.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }

    // internal class Teams
    // {
    // }

    // internal class CurrentPlayers
    // {
    // }
}