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
            /*// Retrieve the UserID from session
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                // Redirect to login if UserID is not in session
                return RedirectToAction("Index", "Home");
            }

            // Filter the dashboard data for the logged-in user
            var userDashboard = ctx.Dashboards
                .Where(d => d.UserID == userId) // Filter by UserID
                .Select(d => new Dashboard
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Age = d.Age,
                    LastReading = d.LastReading
                })
                .ToList();

            return View(userDashboard);*/



            // Retrieve UserID from TempData or Session (depending on how you're storing it)
            /*if (TempData["UserID"] != null)
            {
                int userId = (int)TempData["UserID"];

                // Get the dashboard information for the logged-in user
                var userDashboard = ctx.Dashboards
                    .Where(d => d.UserID == userId)
                    .Select(d => new
                    {
                        d.FirstName,
                        d.LastName,
                        d.Age,
                        d.LastReading
                    }).ToList();

                // Pass the data to the view
                return View(userDashboard);
            }

            // If no UserID is found, redirect to the login page
            return RedirectToAction("Index", "Home");*/



            // Get the logged-in user's username from TempData
            /*var username = TempData[UserID]?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home"); // Redirect to login if no user is logged in
            }

            // Query the database for the logged-in user's dashboard data
            var userDashboard = ctx.Dashboards
                .Where(d => d.UserID == username) // Filter by UserID
                .ToList();

            return View(userDashboard);*/





            var dash = ctx.Dashboards
            .Select(d => new Dashboard // Ensure you map to the Dashboard model
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Age = d.Age
            }).ToList();

            return View(dash);   //THIS IS THE LAST ONE THAT WORKED!!!
        }






        /*public IActionResult Dashboard()
        {
            var dash = ctx.Dashboards
                .Include(d => d.RelatedEntity) // Replace with the navigation property
                .ToList();

            return View(dash);
        }*/
    }
}
