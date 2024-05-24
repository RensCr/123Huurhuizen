using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.interfaces
{
    public interface IUserRepository
    {
        public bool CreateAccount(User user);
        public bool CheckIfUserExist(string name, string hashedPassword, out int userId);
        public string GetUserName(int id);
    }
}
