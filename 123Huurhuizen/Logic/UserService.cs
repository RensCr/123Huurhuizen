using Logic.interfaces;
using Logic.dtos;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Logic
{
    public class UserService : IUserService
    {
        private Account account;

        public bool TryAuthenticateUser(LoginDto loginDto, out int userId)
        {
            string hashedPassword = account.HashPassword(loginDto.Password);
            if (account.IsValidUser(loginDto.Email, hashedPassword, out userId))
            {
                return true;
            }
            return false;
        }
        public string GetTokenInformation(string email, int userId)
        {
            string username = account.GetUserName(userId);
            int expirationTime = 7;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("jDv3wF1oZTcX7rEm5qHlA4N8kGyS9iP2uWbO6sYtLxKzJgRnU0fDpVQeCbIaMh"); // Replace with your secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Email", email),
                    new Claim("Name", username),
                    new Claim("Id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenstring = tokenHandler.WriteToken(token);
            return tokenstring;
        }

    }
}
