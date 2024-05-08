using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.interfaces
{
    public interface IUserRepository
    {
        public bool CreateAccount(string name, string email, string hashedPassword, bool doesUserWantToSell, bool? companyRent);
        public bool CheckIfUserExist(string name, string hashedPassword, out int userId);
    }
}
