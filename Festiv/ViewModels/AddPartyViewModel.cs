using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Festiv.Models;

namespace Festiv.ViewModels;

public class AddPartyViewModel
{
    [Required(ErrorMessage = "Event Name is Required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 50 characters")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Description should be less than 500 characters")]
    public string? Description { get; set; }

    
    [Required(ErrorMessage = "Location is Required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Location should be between 3 and 50 characters")]
    public string? Location { get; set; }

    [Required(ErrorMessage = "Start is Required")]
    public DateTime Start { get; set; }

    [Required(ErrorMessage = "End is Required")]
    public DateTime End { get; set; }

    public string? Photo { get; set; }

    public Models.Host? PartyHost { get; set; }
    public List<Guest>? GuestList { get; set; }
    public List<Game>? Games { get; set; }

    // public Photo? Photo { get; set; }
    
}