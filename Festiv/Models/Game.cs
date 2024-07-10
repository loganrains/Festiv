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

        public List<User> WaitingPlayers { get; set; }

        public List<User> CurrentPlayers { get; set; }

        public List<List<User>> Teams { get; set; }
        public Game()
        {
        }
        public Game(string gameName, List<User> waitingPlayers, List<User> currentPlayers, List<User>teams, int minPlayers, int maxplayers)
        {
            GameName = gameName;
            MinPlayers = minPlayers;
            MaxPlayers = maxplayers;
            WaitingPlayers = waitingPlayers ?? new List<User>();
            CurrentPlayers = currentPlayers ?? new List<User>();
            Teams = new List<List<User>>();
        }

        // public Game()
        // {
        //     WaitingPlayers = new List<User>();
        //     CurrentPlayers = new List<User>();
        //     Teams = new List<List<User>>();
        // }

        

        public override string ToString()
        {
            return GameName;
        }

        public override bool Equals(object? obj)
        {
            return obj is Game @game && 
                Id == @game.Id;
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