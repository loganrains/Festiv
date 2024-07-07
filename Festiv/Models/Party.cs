using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Festiv.Models;

public class Party
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Host PartyHost { get; set; }
    public List<Guest> GuestList { get; set; }
    public int Id { get; set; }
    static private int nextId = 1;

    public Party()
    {
        Id = nextId;
        nextId++;
    }

    public Party(string name, string description, string location, DateTime start, DateTime end)
    {
        Name = name;
        Description = description;
        Location = location;
        Start = start;
        End = end;
        Id = nextId;
        nextId++;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is Party @party && Id == @party.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
