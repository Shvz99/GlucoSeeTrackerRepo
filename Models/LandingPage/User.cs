using System.ComponentModel.DataAnnotations;

namespace GlucoSeeTracker.Models.LandingPage
{
    public class User
    {
        public int UserId { get; set; } //Primary key

        [Required(ErrorMessage = "Please enter a username")]
        public string? Username { get; set; }

        
    }

    public class Password
    { 
        public string? PasswordId { get; set; }
    }
}