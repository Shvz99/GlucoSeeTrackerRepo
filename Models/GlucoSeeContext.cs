using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

namespace GlucoSeeTracker.Models
{
    public class GlucoSeeContext : DbContext
    {
        public GlucoSeeContext(DbContextOptions<GlucoSeeContext> options) 
        : base (options) { }  //

        public DbSet<Landing> Landings { get; set; } //property
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<GlucoRecord> GlucoRecords { get; set;}

        public enum MealTime {Before, After}; 

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Data Seeding
        {
            modelBuilder.Entity<Landing>().HasData(
                new Landing
                {
                    UserID = 1,
                    Username = "First",
                    Password = "1nh0",
                },
                new Landing 
                { 
                    UserID = 2,
                    Username = "Second",
                    Password = "2nh0"
                },
                new Landing
                {
                    UserID = 3, 
                    Username = "Third", 
                    Password = "3nh0"
                }
                );

            modelBuilder.Entity<Dashboard>().HasData(
                new Dashboard
                {
                    FirstName = "Will",
                    LastName = "Smith",
                    Age = 40,
                    LastReading = 7,
                },
                new Dashboard
                {
                    FirstName = "Sam",
                    LastName = "Smith",
                    Age = 30,
                    LastReading = 8.5m,
                },
                new Dashboard
                {
                    FirstName = "Taylor",
                    LastName = "Swift",
                    Age = 30,
                    LastReading = 5.6m,
                }
                ) ;

            modelBuilder.Entity<GlucoRecord>().HasData(
                new GlucoRecord
                {
                    ReadingID = 1,
                    UserID = 1,
                    GlucoLevel = 7.5m,
                    DateTime = new (12/03/2022 /*08:00:00*/),
                    Meal = GlucoRecord.MealTime.Before
                }
                );
        }
    }
}
