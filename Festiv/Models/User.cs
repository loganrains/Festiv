namespace Festiv.Models;

public class User
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public string? Email {get; set;}
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? ProfilePic {get; set;}
    public int? Rating {get; set;}
    public bool UserType {get; set;}
    public User(int id, string? name, string? email, string? firstName, string? lastName, string? profilePic, int? rating, bool userType)
    {
        Id = id;
        Name = name;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ProfilePic = profilePic;
        Rating = rating;
        UserType = userType;
    }
}
