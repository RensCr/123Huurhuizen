

using JwtToken;
using Logic;
using Logic.dtos;
using Logic.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Logincheck
            logincheck = new(); //Make sure to use this in all Http methods to check if the user is logged in
        private  IHouseService houseService;
        private IAccount account;
        public HomeController(ILogger<HomeController> logger,IHouseService houseService,IAccount account)
        {
            _logger = logger;
            this.houseService = houseService;
            this.account = account;
        }

        public IActionResult Index()
        {
            List<HouseDto> houses = houseService.GetAllHouses();
            int SellerId = logincheck.GetSellerId(Request);
            ViewBag.SellerId = SellerId;
            return View(new HouseOverviewViewModel(houses));

        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Home(int id)
        {
            if (!logincheck.CheckValidJwtToken(Request))
            {

                return View("~/Views/Account/Login.cshtml");
            }
            if (houseService.checkIfHouseExist(id))
            {
                return View(new HouseInformationViewModel(houseService.GetHouseInformationOverview(id)));
            }
            return RedirectToAction("index");
        }
        public IActionResult AddHouse()
        {
            if (logincheck.CheckValidJwtToken(Request) )
            {
                if (account.IsUserSeller(logincheck.GetSellerId(Request)))
                {
                    List<LoadPropertiesDto> AvailableProperties = houseService.GetAvailableProperties();
                    return View(new AddHouseInformationViewModel(AvailableProperties));
                }
            }
            return View("~/Views/Account/Login.cshtml");
        }
        [HttpPost]
        public IActionResult AddHouse(AddHouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                int createdHouseId = houseService.AddHouse(CreateHouseInformation(model));
                houseService.AddHousePictures(createdHouseId, PhotoPublisher(model.photos));
                houseService.SetHouseProperties(createdHouseId, model.Properties);

                // Redirect naar de Home-actie met het meegegeven ID
                return RedirectToAction("Home", new { id = createdHouseId });
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
            UpdateHouseDto updatedHouseDto = new UpdateHouseDto(houseId, rentPerMonth, availableAt);
            bool result = houseService.UpdateHouse(updatedHouseDto);
            return Json(new { success = result });
        }

        private AddHouseInformationDto CreateHouseInformation(AddHouseViewModel model)
        {
            return new AddHouseInformationDto(
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
                photosLinks.Insert(0,photoLink);
            }
            return photosLinks;
        }
    }
}
