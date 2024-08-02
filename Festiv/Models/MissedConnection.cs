// Models/MissedConnection.cs
using System.ComponentModel.DataAnnotations;

namespace Festiv.Models
{
    public class MissedConnection
    {
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? PersonDescription { get; set; }

        [Required]
        public string? ContactInfo { get; set; }
    }
}
