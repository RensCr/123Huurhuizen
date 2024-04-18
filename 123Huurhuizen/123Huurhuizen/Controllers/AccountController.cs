﻿using Microsoft.AspNetCore.Mvc;
using Core;

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
                Database database = new();
                string hashedPassword = database.HashPassword(password);
                try
                { 
                    database.AddAccount(name,email, hashedPassword, checkboxForRent,companyRent);
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
