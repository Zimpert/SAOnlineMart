using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Models;
using System.Diagnostics;

namespace SAOnlineMart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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
        // login actions
        public IActionResult BuyerHome()
        {
            ViewData["Greeting"] = $"Hello {HttpContext.Session.GetString("UserName")}";
            return View();
        }

        public IActionResult AdminHome()
        {
            ViewData["Greeting"] = $"Hello {HttpContext.Session.GetString("UserName")}";
            return View();
        }

        public IActionResult SellerHome()
        {
            ViewData["Greeting"] = $"Hello {HttpContext.Session.GetString("UserName")}";
            return View();
        }
    }
}
