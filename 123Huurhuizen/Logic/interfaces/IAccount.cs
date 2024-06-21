using Logic.dtos;

namespace Logic.interfaces
{
    public interface IAccount
    {
        bool AddAccount(UserDto user);
        string GetUserName(int userId);
        string HashPassword(string password);
        bool IsValidUser(string email, string hashedPassword, out int userId);
        public bool IsUserSeller(int id);
    }
}