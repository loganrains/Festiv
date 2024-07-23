using System.Collections.Generic;

namespace Festiv.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public List<User> Members { get; set; } = new List<User>();

        public Team() { }

        public Team(int gameId)
        {
            GameId = gameId;
        }

        public override bool Equals(object? obj)
        {
            return obj is Team team &&
                Id == team.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}