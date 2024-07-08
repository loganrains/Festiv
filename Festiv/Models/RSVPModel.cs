using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Festiv.Models;
using System.ComponentModel.DataAnnotations;

namespace PartyRsvp.Models
{
    public class GuestRespond
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please specify whether you will attend")]
        public bool? Attend { get; set; }

    }
}