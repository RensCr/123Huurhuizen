using _123Huurhuizen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace _123Huurhuizen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Logincheck
            logincheck = new(); //Make sure to use this in all Http methods to check if the user is logged in

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
        public IActionResult Home()
        {
            if (!logincheck.CheckValidJwtToken(Request))
            {
                return View("~/Views/Account/Login.cshtml");
            }
            return View();
        }
        public IActionResult AddHouse()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
