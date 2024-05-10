using _123Huurhuizen.Models;
using Dal;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IHouseRepository houseRepository = new HouseRepository();
            List<Photo> photos = houseRepository.GetFirstHousePicture();
            List<double> PricesPerMonth = houseRepository.GetRentPricePerMonth();
            List<string> StartDates = houseRepository.GetStartRentedDays();
            List<string> Renters = houseRepository.GetSortRenter(); // Of een andere gewenste lege lijst
            return View(new HouseViewModel(photos, PricesPerMonth, StartDates, Renters));
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
            HouseInformation houseInfo = new HouseInformation(location, price, date.Date, information);
            House house = new House();
            IHouseRepository houseRepository = new HouseRepository();
            int CreatedHouseId = house.GiveHouseInformationToDal(houseInfo, houseRepository);
            house.GiveHousePicturesToDal(CreatedHouseId, PhotosLink, houseRepository);
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
