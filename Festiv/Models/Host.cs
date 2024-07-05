using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Festiv.Models;

public class Host
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Host(string firstname, string lastname, string email)
    {
        FirstName = firstname;
        LastName = lastname;
        Email = email;

    }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}