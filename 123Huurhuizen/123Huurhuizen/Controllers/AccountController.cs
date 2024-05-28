using Microsoft.AspNetCore.Mvc;
using Logic.interfaces;
using Logic;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logic.models;
using _123Huurhuizen.Models;

namespace _123Huurhuizen.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Logincheck
            logincheck = new(); //Make sure to use this in all Http methods to check if the user is logged in

        public AccountController(ILogger<HomeController> logger,IUserRepository userRepository,IHouseRepository houseRepository)
        {
            _logger = logger;
            Account = new Account(userRepository);
            userService = new UserService(userRepository);
            houseService = new HouseService(houseRepository);
        }
        private Account Account;
        private HouseService houseService;
        private UserService userService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email, string password)
        {
            if (userService.TryAuthenticateUser(Email, password, out int userId))
            {
                GenerateAndSetJwtToken(Email, userId);
                List<House> houses = houseService.GetAllHouses();
                ViewBag.SellerId = userId;
                return View("~/Views/Home/Index.cshtml", new HouseViewModel(houses));
            }

            return View();
        }


        private void GenerateAndSetJwtToken(string email, int userId)
        {
            int expirationTime = 7;
            Response.Cookies.Append("jwtToken", userService.GetTokenInformation(email,userId), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(expirationTime),
                HttpOnly = true 
            });
        }


        [HttpPost]
        public IActionResult Create(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.RepeatedPassword)
                {
                    string hashedPassword = Account.HashPassword(model.Password);
                    try
                    {
                        User user = new User(model.Name, model.Email, hashedPassword, model.CheckboxForRent, model.CompanyRent);
                        Account.AddAccount(user);
                    }
                    catch (Exception ex)
                    {
                    }

                    return View("~/Views/Account/Login.cshtml");
                }
            }
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies["jwtToken"] != null)
            {
                // Set the cookie to expire immediately
                Response.Cookies.Append("jwtToken", "", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    HttpOnly = true,
                });
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
