using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool DoesUserWantToSell { get; set; }
        public bool? CompanyRent { get; set; }
        public UserDto(string name, string email, string hashedPassword, bool doesUserWantToSell, bool? companyrent = false)
        {
            Name = name;
            Name = name;
            Email = email;
            HashedPassword = hashedPassword;
            DoesUserWantToSell = doesUserWantToSell;
            CompanyRent = companyrent;
        }
    }
}

