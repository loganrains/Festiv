using System.ComponentModel.DataAnnotations;

namespace Festive.Models
{
    public class MissedConnections
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? ContactInfo { get; set; }
    }
}