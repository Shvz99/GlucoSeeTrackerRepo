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

        //show the log-in/Index page
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

            // Store UserID for later use
            /* TempData["UserID"] = user.UserID;*/

            // Store UserID in the session
            /*HttpContext.Session.SetInt32("UserID", user.UserID);*/
            return RedirectToAction("Dashboard", "GlucoSee"); // Replace "Dashboard" with your target controller
        }


        //REGISTRATION!!!
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register", new Landing());
            /*return View();*/
        }

        [HttpPost]
        public IActionResult Register(Landing register)
        {
            if (string.IsNullOrEmpty(register.Username) || string.IsNullOrEmpty(register.Password))
            {
                ViewBag.Error = "Please provide both username and password.";
                return View("Register", register);
            }

            // Check if the user already exists in the database
            var userExists = _context.Landings.Any(u => u.Username == register.Username);
            if (userExists)
            {
                ViewBag.Error = "User already exists. Please use a different username.";
                return View("Register", register); // Pass the model back to the view
            }
            // Add the new user to the database
            _context.Landings.Add(register);
            _context.SaveChanges();

            // Redirect to the Login page (Index action in HomeController)
            return RedirectToAction("Index", "Home");
        }
    }
}
