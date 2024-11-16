using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlucoSeeTracker.Models
{
    public class GlucoRecord
    {
        public int ReadingID { get; set; } //PK

        [Required(ErrorMessage = "Please enter blood glucose level")]
        public decimal GlucoLevel { get; set; }

        [Required(ErrorMessage = "Please enter the date")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please specify if glucose level was measured before or after meal")]
        public string PrePostMeal { get; set; } = string.Empty;

        //FK
        /*[ForeignKey("Dashboard")] - this is not needed in this case*/
        [Required(ErrorMessage = "DashID is required")]
        public int DashID { get; set; }
        //Navigation property for UserID
        [ValidateNever]
        public Dashboard Dashboard { get; set; } = null!;
    }
}
