using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Festiv.Models;

public class Party
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set;}

    public Party(string name, string description, string location)
    {
        Name = name;
        Description = description;
        Location = location;
    }

    public override string ToString()
    {
        return Name + Description + Location;
    }

}
