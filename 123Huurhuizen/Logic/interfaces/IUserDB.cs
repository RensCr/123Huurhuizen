using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.interfaces
{
    public interface IUserDB
    {
        public bool CreateAccount(string name, string email, string hashedPassword, bool doesUserWantToSell, bool? companyRent);
    }
}
