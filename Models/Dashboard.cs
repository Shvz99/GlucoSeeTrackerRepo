using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlucoSeeTracker.Models
{
    public class Dashboard
    {
        public int DashID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal LastReading { get; set; }

        //userID - FK 
        /*[ForeignKey("Landing")]*/
        [Required(ErrorMessage = "UserID is required")]
        public int UserID { get; set; }
        //Navigation property for UserID
        public Landing Landing { get; set; } = null!;
    }
}
