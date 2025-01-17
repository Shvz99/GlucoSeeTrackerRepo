using GlucoSeeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GlucoSeeTracker.Controllers
{
    public class GlucoSeeController : Controller
    {
        private readonly GlucoSeeContext ctx;
        private readonly ILogger<GlucoSeeController> _logger;
        public GlucoSeeController(GlucoSeeContext context, ILogger<GlucoSeeController> logger)
        {
            ctx = context;
            _logger = logger;
        }

        public IActionResult Dashboard()
        {
            /*var dash = ctx.Dashboards
            .Select(d => new Dashboard // Ensure you map to the Dashboard model
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Age = d.Age
            }).ToList();

            return View(dash);*/   //THIS IS THE LAST ONE THAT WORKED!!!



            // Check if the user is logged in
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                // Redirect to Login if no session exists
                return RedirectToAction("Index", "Home");
            }

            // Retrieve user data from Landing and Dashboard tables
            var landing = ctx.Landings.FirstOrDefault(l => l.Username == username);

            if (landing == null)
            {
                /*return View("Error"); // User not found*/ //Commented out

                // Handle invalid user (e.g., remove session and redirect)
                HttpContext.Session.Remove("Username");
                return RedirectToAction("Index", "Home");
            }

            var dashboard = ctx.Dashboards.FirstOrDefault(d => d.UserID == landing.UserID);

            // Handle null dashboard gracefully
            if (dashboard == null)
            {
                // Create a new Dashboard object with default values
                dashboard = new Dashboard
                {
                    FirstName = "",
                    LastName = "",
                    Age = 0
                };
            }

            var userReadings = ctx.GlucoRecords
                        .Where(r => r.Dashboard.UserID == landing.UserID)
                             .OrderByDescending(r => r.DateTime)
                             .ToList();

            var distinctReadings = userReadings.GroupBy(r => new { r.ReadingID, r.DateTime })
                                      .Select(g => g.First())
                                      .ToList();

            // Retrieve the latest reading
            var newReading = distinctReadings.FirstOrDefault();

            // Create and pass the ViewModel
            var dashboardViewModel = new DashboardViewModel
            {
                Username = landing.Username,
                FirstName = dashboard.FirstName,
                LastName = dashboard.LastName,
                Age = dashboard.Age,
                NewReading = newReading,
                AllReadings = distinctReadings
            };

            return View(dashboardViewModel);
        }

        [HttpGet]
        public IActionResult Update() //4.1.25 (NEW)
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                // Handle case where user is not logged in
                return RedirectToAction("Index", "Home");
            }

            var landing = ctx.Landings.FirstOrDefault(l => l.Username == username);

            if (landing == null)
            {
                // Handle case where user is not found
                return NotFound();
            }

            var dashboard = ctx.Dashboards.FirstOrDefault(d => d.UserID == landing.UserID);

            if (dashboard == null)
            {
                // Create a new dashboard if it doesn't exist
                dashboard = new Dashboard
                {
                    UserID = landing.UserID
                };
                ctx.Dashboards.Add(dashboard);
                ctx.SaveChanges();
            }

            var updateViewModel = new DashboardViewModel
            {
                FirstName = dashboard.FirstName,
                LastName = dashboard.LastName,
                Age = dashboard.Age
            };

            return View(updateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(DashboardViewModel model)
        {
                // Get the current username from the session
                var username = HttpContext.Session.GetString("Username");

                // Retrieve the user's Landing and Dashboard information
                var landing = ctx.Landings.FirstOrDefault(l => l.Username == username);
                var dashboard = ctx.Dashboards.FirstOrDefault(d => d.UserID == landing!.UserID);

                if (landing == null || dashboard == null)
                {
                    // Handle the case where the user or their dashboard is not found
                    return NotFound();
                }

            // Update Dashboard properties with values from the model
            dashboard.FirstName = model.FirstName;
            dashboard.LastName = model.LastName;
            dashboard.Age = model.Age;

            // Save changes to the database
            ctx.SaveChanges();

            // Redirect to the Dashboard action
            return RedirectToAction("Dashboard");

            /*// Create a new GlucoRecord object and assign the dashboard
            var glucoRecord = new GlucoRecord { Dashboard = dashboard };

            // Retrieve all previous readings for the user
            var userReadings = ctx.GlucoRecords
                .Where(r => r.Dashboard.UserID == landing.UserID)
                .OrderByDescending(r => r.DateTime)
                .ToList();

            // Create a view model to pass both the new GlucoRecord and existing readings
            var addViewModel = new DashboardViewModel
            {
                NewReading = glucoRecord,
                AllReadings = userReadings
            };

            return View(addViewModel);*/
        }

        public IActionResult Add()
        {
            // Get the current username from the session
            var username = HttpContext.Session.GetString("Username");

            // Retrieve the user's Landing and Dashboard information
            var landing = ctx.Landings.FirstOrDefault(l => l.Username == username);
            var dashboard = ctx.Dashboards.FirstOrDefault(d => d.UserID == landing!.UserID);

            if (landing == null || dashboard == null)
            {
                // Handle the case where the user or their dashboard is not found
                return NotFound();
            }

            // Create a new GlucoRecord object and assign the dashboard
            var glucoRecord = new GlucoRecord { Dashboard = dashboard };

            return View(glucoRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(GlucoRecord glucoRecord)
        {
            if (ModelState.IsValid)
            {
                // Get the current username from the session
                var username = HttpContext.Session.GetString("Username");

                // Retrieve the user's Landing and Dashboard information
                var landing = ctx.Landings.FirstOrDefault(l => l.Username == username);
                var dashboard = ctx.Dashboards.FirstOrDefault(d => d.UserID == landing!.UserID);

                if (landing == null || dashboard == null)
                {
                    // Handle the case where the user or their dashboard is not found
                    return NotFound();
                }

                // Ensure the Dashboard is correctly assigned to the GlucoRecord
                glucoRecord.Dashboard = dashboard;

                ctx.Add(glucoRecord);
                ctx.SaveChanges();

                return RedirectToAction("Dashboard");
            }

            return View(glucoRecord);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var record = ctx.GlucoRecords.Find(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Delete(GlucoRecord record)
        {
            ctx.GlucoRecords.Remove(record);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glucoRecord = await ctx.GlucoRecords.FindAsync(id);

            if (glucoRecord == null)
            {
                return NotFound();
            }

            return View(glucoRecord);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GlucoRecord record)
        {
            if (id != record.ReadingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var recordToUpdate = await ctx.GlucoRecords.FirstOrDefaultAsync(r => r.ReadingID == id);

                    if (recordToUpdate == null)
                    {
                        return NotFound();
                    }

                    recordToUpdate.GlucoLevel = record.GlucoLevel;
                    recordToUpdate.DateTime = record.DateTime;
                    recordToUpdate.PrePostMeal = record.PrePostMeal;

                    await ctx.SaveChangesAsync();

                    return RedirectToAction("Dashboard");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the GlucoRecord.");
                    return View("Error");
                }
            }

            return View(record);
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GlucoRecord record)
        {
            if (id != record.ReadingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var recordToUpdate = await ctx.GlucoRecords.FindAsync(id);

                    if (recordToUpdate == null)
                    {
                        return NotFound();
                    }

                    recordToUpdate.GlucoLevel = record.GlucoLevel;
                    recordToUpdate.DateTime = record.DateTime;
                    recordToUpdate.PrePostMeal = record.PrePostMeal;

                    await ctx.SaveChangesAsync();

                    return RedirectToAction("Dashboard");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the GlucoRecord.");
                    return View("Error");
                }
            }

            return View(record);
        }
    }
}