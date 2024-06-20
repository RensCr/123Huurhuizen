using Logic.dtos;
using Logic.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Mockdata
{
    public class MockAccountService : IAccount
    {
        public bool AddAccount(UserDto user)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(int userId)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password)
        {
            if(password == "test")
            {
                return "test";
            }
            else
            {
                return "test2";
            }
        }

        public bool IsUserSeller(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsValidUser(string email, string hashedPassword, out int userId)
        {
            throw new NotImplementedException();
        }
    }
}
