using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Festiv.Models;

public class Guest
{
    public int Id {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public int Id { get; set; }
    static private int nextId = 1;

    public Guest() { }

    public Guest(string firstname, string lastname, string email)
    {
        FirstName = firstname;
        LastName = lastname;
        Email = email;

    }

    public Guest()
    {
    }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}