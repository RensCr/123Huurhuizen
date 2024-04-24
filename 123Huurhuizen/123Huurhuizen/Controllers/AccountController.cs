using Microsoft.AspNetCore.Mvc;
using Dal;
using Logic.interfaces;
using Logic;

namespace _123Huurhuizen.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Aanmaak()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Aanmaak(string name,string email, string password, string repeatedpassword,bool checkboxForRent, bool? companyRent)
        {
            if (password == repeatedpassword)
            {
                IUserDB userDB = new UserDB();
                Account account = new Account();
                string hashedPassword = account.HashPassword(password);
                try
                {
                    account.AddAccount(name,email, hashedPassword, checkboxForRent,companyRent,userDB);
                }
                catch (Exception ex) { }

                return Redirect("https://www.youtube.com");
            }
            else
            {
                return View();
            }
        }

    }
}
