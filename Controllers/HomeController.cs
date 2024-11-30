using GlucoSeeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace GlucoSeeTracker.Controllers
{
    public class HomeController : Controller
    {
       /* private GlucoSeeContext context { get; set; } = null!;
        public HomeController(GlucoSeeContext ctx)
        {
            context = ctx;
        }*/

        public IActionResult Index()  //return landing/login page
        {
            /*var landing = context.Landings;*/
            return View("Index");
        }

/*
        [HttpGet] //Display the registration form
        public IActionResult Register()
        {
            // Prepare the form for registration
            ViewBag.Action = "Register";
            return View(new Landing());
        }

        [HttpPost]
        public async Task<IActionResult> Register(Landing landing)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingUser = await context.Landings.FirstOrDefaultAsync(u => u.Username == landing.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "This Username is taken.");
                    return View(landing);
                }

                // Save new record
                await context.Landings.AddAsync(landing);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            // Return form with errors
            return View("Register", landing);
        }*/
    }
}
