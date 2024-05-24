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
            houseService = new HouseService(houseRepository);


        }
        private Account Account;
        private HouseService houseService;

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
            string hashedPassword = Account.HashPassword(password);
            if (Account.IsValidUser(Email, hashedPassword, out int userId))
            {
                string Username = Account.GetUserName(userId);
                int expirationTime = 7;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(
                    "jDv3wF1oZTcX7rEm5qHlA4N8kGyS9iP2uWbO6sYtLxKzJgRnU0fDpVQeCbIaMh");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Email", Email),
                    new Claim("Name", Username),
                    new Claim("Id", userId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(expirationTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenstring = tokenHandler.WriteToken(token);
                Response.Cookies.Append("jwtToken", tokenstring, new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(expirationTime),
                    HttpOnly = true //Cookie can only be found in an http request
                });
                List<House> houses = houseService.GetAllHouses();

                ViewBag.SellerId = userId;
                return View("~/Views/Home/Index.cshtml", new HouseViewModel(houses));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Aanmaak(string name, string email, string password, string repeatedpassword, bool checkboxForRent, bool? companyRent)
        {
            if (password == repeatedpassword)
            {

                string hashedPassword = Account.HashPassword(password);
                try
                {
                    User user = new User(name, email, hashedPassword, checkboxForRent, companyRent);
                    Account.AddAccount(user);
                }
                catch (Exception ex) { }

                return View("~/Views/Account/Login.cshtml");
            }
            else
            {
                return View();
            }
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
