using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Festiv.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string? GameName { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public List<User> WaitingPlayers { get; set; } = new List<User>();
        public List<User> CurrentPlayers { get; set; } = new List<User>();
        public List<Team> Teams { get; set; } = new List<Team>();
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
                GameId == game.GameId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GameId);
        }

    }

    // internal class Teams
    // {
    // }

    // internal class CurrentPlayers
    // {
    // }
}