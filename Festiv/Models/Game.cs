using System;
namespace Festiv.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; } 

        public List<User> CurrentPlayers { get; set; }

        public List<List<User>> Teams { get; set; }

        public Game(string name, List<User> currentPlayers, List<User>teams)
        {
            Name = name;
            CurrentPlayers = new List<currentPlayers>();
            Teams = new List<List<teams>>();
        }

        public Game()
        {
            Users = new List<User>();
            CurrentPlayers = new List<User>();
            Teams = new List<List<User>();
        }

        public override string ToString()
        {
            return Name;
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
}