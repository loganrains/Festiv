using Microsoft.AspNetCore.Identity;
using Festiv.Data;

namespace Festiv.Models;


//No Id because need to properly extend identityuser and update DbContext//

public class User : IdentityUser<Guid>
{
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? ProfilePic {get; set;}
    public int Rating {get; set;}
    public bool UserType {get; set;}
    public ICollection<GuestRespond> GuestResponds { get; set; } = new List<GuestRespond>();
    public ICollection<Gift> Gifts { get; set; } = new List<Gift>();
    public ICollection<Game> WaitingPlayers { get; set; } = new List<Game>();
    public ICollection<Game> CurrentPlayers { get; set; } = new List<Game>();
    public User( bool userType, string? firstName, string? lastName, string? profilePic, int rating = 1000)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfilePic = profilePic;
        Rating = rating;
        UserType = userType;
    }
        public User()
    {
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
