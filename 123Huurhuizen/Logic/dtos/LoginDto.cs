using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginDto(string email,string password) 
        {
            Email = email;
            Password = password;
        }
    }
}
