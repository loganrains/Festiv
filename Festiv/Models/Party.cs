using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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


    public Party(string name, string description, string location, DateTime start, DateTime end)
    {
        Name = name;
        Description = description;
        Location = location;
        Start = start;
        End = end;
    }

    public override string ToString()
    {
        return Name;
    }

}
