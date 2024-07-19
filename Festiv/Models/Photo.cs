using System.ComponentModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Festiv.Models
{
    public class Photo
    {
        public string? Link { get; set; }

        public string? AltText { get; set; }
    
        public int Id { get; set; }

        public Photo()
        {
        }

        public Photo(string link, string altText)
        {
        Link = link;
        AltText = altText;
        }

        public override string ToString()
        {
            return AltText;
        }
    }
}