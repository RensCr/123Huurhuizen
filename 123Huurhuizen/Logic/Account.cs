using System.Text;
using System.Security.Cryptography;
using Logic.interfaces;
using Logic.dtos;

namespace Logic
{
    public class Account : IAccount
    {
        private readonly IUserRepository _userRepository;
        public Account(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

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

        public bool AddAccount(UserDto user)
        {
            try
            {
                _userRepository.CreateAccount(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsValidUser(string email, string hashedPassword, out int userId)
        {
            if (_userRepository.CheckIfUserExist(email, hashedPassword, out int user))
            {
                userId = user;
                return true;
            }
            else
            {
                userId = -1;
                return false;
            }

        }

        public string GetUserName(int userId)
        {
            string UserName = _userRepository.GetUserName(userId);
            return UserName;
        }

        public bool IsUserSeller(int id) => _userRepository.IsUserSeller(id);

    }
}
