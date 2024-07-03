using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Festiv.Models;

public class Party
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string StartTime { get; set; }
    public string StartDate { get; set; }
    public string EndTime { get; set; }
    public string EndDate { get; set; }

    public Party(string name, string description, string location, string starttime, string startdate, string endtime, string enddate )
    {
        Name = name;
        Description = description;
        Location = location;
        StartTime = starttime;
        StartDate = startdate;
        EndTime = endtime;
        EndDate = enddate;
    }

    public override string ToString()
    {
        return "The " + Name + " will be " + Description + " that takes place at " + Location;
    }
}
