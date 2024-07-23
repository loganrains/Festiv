using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Festiv.Models
{
    public class Party
    {
        public string? Name { get; set; }

        public PartyDetails? Details { get; set; }
        
        // public int DetailsId { get; set; }
    
    public int Id { get; set; }

    public Party()
    {
    }

    public Party(string name, string description, string location, DateTime start, DateTime end, PartyDetails partyDetails)
    {
        Name = name;
        Details = partyDetails;
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