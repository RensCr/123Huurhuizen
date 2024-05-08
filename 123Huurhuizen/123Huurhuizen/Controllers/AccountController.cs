using Microsoft.AspNetCore.Mvc;
using Dal;
using Logic.interfaces;
using Logic;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _123Huurhuizen.Controllers
{
    public class AccountController : Controller
    {
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
            IUserRepository userDB = new UserRepository();
            Account account = new Account();
            string hashedPassword = account.HashPassword(password);
            if (account.IsValidUser(Email, hashedPassword, userDB,out int userId))
            {
                int expirationTime = 7;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(
                    "jDv3wF1oZTcX7rEm5qHlA4N8kGyS9iP2uWbO6sYtLxKzJgRnU0fDpVQeCbIaMh");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Email", Email),
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

                return View("~/Views/Home/Index.cshtml");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Aanmaak(string name,string email, string password, string repeatedpassword,bool checkboxForRent, bool? companyRent)
        {
            if (password == repeatedpassword)
            {
                IUserRepository userDB = new UserRepository();
                Account account = new Account();
                string hashedPassword = account.HashPassword(password);
                try
                {
                    account.AddAccount(name,email, hashedPassword, checkboxForRent,companyRent,userDB);
                }
                catch (Exception ex) { }

                return View("~/Views/Account/Login.cshtml");
            }
            else
            {
                return View();
            }
        }

    }
}
