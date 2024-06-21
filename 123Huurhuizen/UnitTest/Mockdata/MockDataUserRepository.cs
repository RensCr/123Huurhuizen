using Logic.dtos;
using Logic.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Mockdata
{
    public class MockDataUserRepository : IUserRepository
    {
        public bool CheckIfUserExist(string name, string hashedPassword, out int userId)
        {
            if(name == "test" && hashedPassword == "test")
            {
                userId = 1;
                return true;
            }
            else
            {
                userId = -1;
                return false;
            }
        }

        public bool CreateAccount(UserDto user)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(int id)
        {
            if(id == 1)
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
    }
}
