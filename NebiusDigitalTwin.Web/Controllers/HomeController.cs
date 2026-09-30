using Microsoft.AspNetCore.Mvc;

namespace NebiusDigitalTwin.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DemoScenario()
        {
            return View();
        }

    }
}
