using Logic.dtos;

namespace Logic.interfaces
{
    public interface IUserService
    {
        string GetTokenInformation(string email, int userId);
        bool TryAuthenticateUser(LoginDto loginDto, out int userId);
    }
}