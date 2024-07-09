using System.ComponentModel.DataAnnotations;
using Festiv.Models;

namespace Festiv.ViewModels;

public class AddPartyViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Models.Host? PartyHost { get; set; }
    public List<Guest>? GuestList { get; set; }
    public int Id { get; set; }
    static private int nextId = 1;
    public List<Game> Games { get; set; }
}