using GlucoSeeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GlucoSeeTracker.Controllers
{
    public class GlucoSeeController : Controller
    {
        private readonly GlucoSeeContext ctx;
        public GlucoSeeController(GlucoSeeContext context)
        {
            ctx = context;
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

            // Create and pass the ViewModel
            var dashboardViewModel = new DashboardViewModel
            {
                Username = landing.Username,
                FirstName = dashboard.FirstName,
                LastName = dashboard.LastName,
                Age = dashboard.Age,
                DashboardData = dashboard
            };

            return View(dashboardViewModel);
        }

    }
}
