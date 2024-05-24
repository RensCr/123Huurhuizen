using _123Huurhuizen.Models;

using Logic;
using Logic.interfaces;
using Logic.models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace _123Huurhuizen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Logincheck
            logincheck = new(); //Make sure to use this in all Http methods to check if the user is logged in

        public HomeController(ILogger<HomeController> logger,IHouseRepository houseRepository)
        {
            _logger = logger;
            this.houseService = new HouseService(houseRepository);
        }
        private readonly HouseService houseService;
        public IActionResult Index()
        {
            List<House> houses = houseService.GetAllHouses();
            int SellerId = logincheck.GetSellerId(Request);
            ViewBag.SellerId = SellerId;
            return View(new HouseViewModel(houses));
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
            if (!logincheck.CheckValidJwtToken(Request))
            {
                return View("~/Views/Account/Login.cshtml");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Addhouse(string location, DateTime date, double price,string information, List<IFormFile> photos)
        {
            PhotoPublisher publisher = new PhotoPublisher();
            List<string> PhotosLink = new List<string>();
            foreach (var photo in photos)
            {
                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }
                string photolink = publisher.UploadImage(imageData);
                PhotosLink.Add(photolink);
            }
            HouseInformation houseInfo = new HouseInformation(logincheck.GetSellerId(Request),location, price, date.Date, information);
            int createdHouseId = houseService.AddHouse(houseInfo);
            houseService.AddHousePictures(createdHouseId, PhotosLink);
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public ActionResult DeleteHouse(int houseId)
        {
            bool result = houseService.DeleteHouse(houseId);
            return Json(new { success = result });
        }
        [HttpPost]
        public ActionResult UpdateHouse(int houseId, double rentPerMonth, DateTime availableAt)
        {
            bool result = houseService.UpdateHouse(houseId,rentPerMonth,availableAt);
            return Json(new { success = result });
        }
    }
}
