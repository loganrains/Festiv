using System.ComponentModel.DataAnnotations;

namespace  Festiv.Models
{
    public class GuestRespond
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public int PartyId { get; set; }

        public Party Party { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        
         public GuestRespond()
        {
            // Parameterless constructor for model binding
        }

        // Constructor to initialize the object
        public GuestRespond(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        // Override ToString() to provide a string representation of the object
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

