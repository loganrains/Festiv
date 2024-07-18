using Festiv.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Festiv.Controllers;

namespace Festiv.ViewModels;



public class UserViewModel
{
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public int? Rating {get; set;}
}
