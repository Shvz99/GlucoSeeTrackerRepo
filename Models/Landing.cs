using System.ComponentModel.DataAnnotations;

namespace GlucoSeeTracker.Models
{
    public class Landing
    {
        public int UserID { get; set; } //Primary Key

        [Required(ErrorMessage = "Please enter a Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
