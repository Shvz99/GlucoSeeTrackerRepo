using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

namespace GlucoSeeTracker.Models
{
    public class GlucoSeeContext : DbContext
    {
        public GlucoSeeContext(DbContextOptions<GlucoSeeContext> options) 
        : base (options) { }  

        public DbSet<Landing> Landings { get; set; } = null!; //property/Tables in DB
        public DbSet<Dashboard> Dashboards { get; set; } = null!;
        public DbSet<GlucoRecord> GlucoRecords { get; set; } = null!;

        /*public enum MealTime {Before, After};*/ //validate in controllers

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Data Seeding
        {
            modelBuilder.Entity<Landing>().HasData(
                new Landing
                {
                    UserID = 1,
                    Username = "First",
                    Password = "Password1",
                },
                new Landing 
                { 
                    UserID = 2,
                    Username = "Second",
                    Password = "Password2"
                },
                new Landing
                {
                    UserID = 3, 
                    Username = "Third", 
                    Password = "Password3"
                }
                );

            modelBuilder.Entity<Dashboard>().HasData(
                new Dashboard
                {
                    DashID = 1,
                    FirstName = "Will",
                    LastName = "Smith",
                    Age = 40,
                    LastReading = 7,
                    UserID = 1,
                },
                new Dashboard
                {
                    DashID = 2,
                    FirstName = "Sam",
                    LastName = "Smith",
                    Age = 30,
                    LastReading = 8.5m,
                    UserID = 2,
                },
                new Dashboard
                {
                    DashID = 3,
                    FirstName = "Taylor",
                    LastName = "Swift",
                    Age = 30,
                    LastReading = 5.6m,
                    UserID = 3,
                }
                ) ;

            modelBuilder.Entity<GlucoRecord>().HasData(
                new GlucoRecord
                {
                    ReadingID = 1,
                    GlucoLevel = 7.5m,
                    DateTime = new (2022, 3, 12, 8, 0, 0), //12.03.2022 08:00
                    PrePostMeal = "Before",
                    DashID = 1,
                }, 
                new GlucoRecord
                {
                    ReadingID = 2, 
                    GlucoLevel = 8.5m, 
                    DateTime = new (2023, 7, 15, 10, 0, 0), //15.07.2023 10:00
                    PrePostMeal = "After", 
                    DashID = 2,
                }, 
                new GlucoRecord
                {
                    ReadingID = 3,
                    GlucoLevel = 7,
                    DateTime = new(2023, 9, 17, 13, 0, 0), //17.09.2023 13:00
                    PrePostMeal = "After",
                    DashID = 3,
                }
                );
        }
    }
}
