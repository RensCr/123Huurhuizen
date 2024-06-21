using System.Text;
using System;
using System.Security.Cryptography;
using Data;

namespace Core
{
    public class Database
    {
        public string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }
        public bool AddAccount(string name,string email, string hashedPassword,bool doesUserWantToSell,bool? companyRent) 
        {
            try
            {
                Data.Database database = new Data.Database();
                database.CreateAccount(name,email, hashedPassword, doesUserWantToSell,companyRent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
