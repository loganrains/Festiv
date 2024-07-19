using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Festiv.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string? GameName { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        [NotMapped]
        public List<User> WaitingPlayers { get; set; } = new List<User>();

        [NotMapped]
        public List<User> CurrentPlayers { get; set; } = new List<User>();

        [NotMapped]
        public List<List<User>> Teams { get; set; } = new List<List<User>>();
        public Game()
        {
        }
        public Game(string gameName, List<User> waitingPlayers, List<User> currentPlayers, List<List<User>> teams, int minPlayers, int maxplayers)
        {
            GameName = gameName;
            MinPlayers = minPlayers;
            MaxPlayers = maxplayers;
            WaitingPlayers = waitingPlayers;
            CurrentPlayers = currentPlayers;
            Teams = teams;
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

    internal class Teams
    {
    }

    internal class CurrentPlayers
    {
    }
}