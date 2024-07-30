using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Festiv.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public PartyDetails? Details { get; set; }
        public int DetailsId { get; set; }
        public ICollection<GuestRespond> GuestResponds { get; set; } = new List<GuestRespond>();
        public ICollection<Gift> Gifts { get; set; } = new List<Gift>();
        public ICollection<Game> Games { get; set; } = new List<Game>();
        public Party()
        {
        }

        public Party(string name, string description, string location, DateTime start, DateTime end, PartyDetails partyDetails)
        {
            Name = name;
            Details = partyDetails;
            DetailsId = Details.Id;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Party @party && Id == @party.Id;
        }

    }
}