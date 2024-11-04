using System.ComponentModel.DataAnnotations;

namespace GlucoSeeTracker.Models
{
    public class GlucoRecord
    {
        public int ReadingID { get; set; } //PK

        [Required(ErrorMessage = "Please enter a UserID.")]
        public int UserID { get; set; } //FK from LandingPage ?
        public Landing Landing { get; set; } = null!;

        [Required(ErrorMessage = "Please enter blood glucose level")]
        public decimal GlucoLevel { get; set; }

        [Required(ErrorMessage = "Please enter the date")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please enter meal time")]
        public MealTime Meal { get; set; }
        public enum MealTime { Before, After };


    }
}
