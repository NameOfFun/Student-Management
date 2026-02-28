using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using System.Diagnostics;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult Users()
        {
            _logger.LogInformation("User accessed the Users action. [Users] Method: {m}", Request.Method);
            return Content("dam Hao dep trai ");
        }
    }
}
