namespace GlucoSeeTracker.Models
{
    public class DashboardViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public Dashboard? DashboardData { get; set; }
        public decimal? LastGlucoseReading { get; set; }
        public GlucoRecord? NewReading { get; set; } 
        public List<GlucoRecord>? AllReadings { get; set; }
        public decimal? NewGlucoseReading { get; set; }
    }
}
