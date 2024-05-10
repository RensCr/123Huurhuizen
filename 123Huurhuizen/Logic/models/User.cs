using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool DoesUserWantToSell {  get; set; }
        public bool? CompanyRent {  get; set; }
        public User(string name,string email,string hashedPassword, bool doesUserWantToSell,bool? companyrent = false)
        {
            this.Name = name;
            this.Name = name;
            this.Email = email;
            this.HashedPassword = hashedPassword;
            this.DoesUserWantToSell = doesUserWantToSell;
            this.CompanyRent = companyrent;
        }
    }
}

