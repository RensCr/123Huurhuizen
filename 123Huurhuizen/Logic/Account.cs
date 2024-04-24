using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Logic.interfaces;

namespace Logic
{
    public class Account
    {
        public string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }
        public bool AddAccount(string name, string email, string hashedPassword, bool doesUserWantToSell, bool? companyRent,IUserDB userDB)
        {
            try 
            {            
                userDB.CreateAccount(name, email, hashedPassword, doesUserWantToSell, companyRent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
