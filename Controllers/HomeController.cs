using GlucoSeeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace GlucoSeeTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly GlucoSeeContext _context;

        // Constructor to inject the database context
        public HomeController(GlucoSeeContext context)
        {
            _context = context;
        }

        //show the log-in page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Landing model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Please provide both username and password.";
                return View();
            }
            // Check if the user exists in the database
            var user = _context.Landings.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
            // User exists: Proceed to dashboard or another page
            TempData["Message"] = $"Welcome, {user.Username}!";
            return RedirectToAction("Index", "Dashboard"); // Replace "Dashboard" with your target controller
        }
    }
}
