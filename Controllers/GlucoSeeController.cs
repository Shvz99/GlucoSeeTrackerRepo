using Microsoft.AspNetCore.Mvc;

namespace GlucoSeeTracker.Controllers
{
    public class GlucoSeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
