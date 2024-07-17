using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Festiv.Models;

public class Host
{
    public int Id {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public int Id { get; set; }
    static private int nextId = 1;

    public Host() { }

    public Host(string firstname, string lastname, string email)
    {
        FirstName = firstname;
        LastName = lastname;
        Email = email;

    }

    public Host()
    {
    }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}