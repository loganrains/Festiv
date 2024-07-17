using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Festiv.Models;

public class PartyDetails
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }

    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }

    public Host? PartyHost { get; set; }
    public List<Guest>? GuestList { get; set; }

    public int Id { get; set; }

    public PartyDetails() { }

    public PartyDetails(string? name, string? description, string? location, DateTime? start, DateTime? end, Host? partyHost, List<Guest>? guestList)
    {
        Name = name;
        Description = description;
        Location = location;
        Start = start;
        End = end;
        PartyHost = partyHost;
        GuestList = guestList;
    }

    public override bool Equals(object? obj)
    {
        return obj is PartyDetails details &&
               Id == details.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

}