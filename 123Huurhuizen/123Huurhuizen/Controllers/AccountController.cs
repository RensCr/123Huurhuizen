using Microsoft.AspNetCore.Mvc;
using Logic.interfaces;
using Logic;
using Logic.dtos;
using JwtToken;
using Models;

namespace Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Logincheck
            logincheck = new(); //Make sure to use this in all Http methods to check if the user is logged in
        private IAccount account;
        private IHouseService houseService;
        private IUserService userService;

        public AccountController(ILogger<HomeController> logger, IUserService userService, IAccount account,IHouseService houseService)
        {
            _logger = logger;
            this.account = account;
            this.userService = userService;
            this.houseService = houseService;
        }


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
            LoginDto loginDto = new LoginDto(Email, password);
            if (userService.TryAuthenticateUser(loginDto, out int userId))
            {
                GenerateAndSetJwtToken(loginDto.Email, userId);
                ViewBag.SellerId = userId;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        private void GenerateAndSetJwtToken(string email, int userId)
        {
            int expirationTime = 7;
            Response.Cookies.Append("jwtToken", userService.GetTokenInformation(email, userId), new CookieOptions
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
                    string hashedPassword = account.HashPassword(model.Password);
                    try
                    {
                        User user = new User(model.Name, model.Email, hashedPassword, model.CheckboxForRent, model.CompanyRent);
                        account.AddAccount(user);
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
