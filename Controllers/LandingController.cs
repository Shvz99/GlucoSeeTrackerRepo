using Microsoft.AspNetCore.Mvc;

namespace GlucoSeeTracker.Controllers
{
    public class LandingController : Controller   //Controller base class provides many properties, methods, and features
                                                  //that handle HTTP requests and produce responses in our application
    {
        public IActionResult Index()
        {
            return View();
        }
    }  //delete
}
