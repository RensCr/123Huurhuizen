using _123Huurhuizen.Models;

using Logic;
using Logic.dtos;
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
        public IActionResult AddHouse(AddHouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                int createdHouseId = houseService.AddHouse(CreateHouseInformation(model));
                houseService.AddHousePictures(createdHouseId, PhotoPublisher(model.photos));

                return RedirectToAction("Index"); 
            }
            return View(model);
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

        private HouseInformation CreateHouseInformation(AddHouseViewModel model)
        {
            return new HouseInformation(
                logincheck.GetSellerId(Request),
                model.Location,
                model.Price,
                model.Date,
                model.Information
            );
        }

        private List<string> PhotoPublisher(List<IFormFile> photos)
        {
            var publisher = new PhotoPublisher();
            var photosLinks = new List<string>();

            foreach (var photo in photos)
            {
                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }
                string photoLink = publisher.UploadImage(imageData);
                photosLinks.Add(photoLink);
            }
            return photosLinks;
        }
    }
}
