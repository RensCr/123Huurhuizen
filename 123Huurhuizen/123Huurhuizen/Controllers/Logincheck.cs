using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Logic;
using Microsoft.IdentityModel.Tokens;

namespace _123Huurhuizen.Controllers
{
    public class Logincheck
    {
        public bool CheckValidJwtToken(HttpRequest request)
        {
            string? jwtString = request.Cookies["jwtToken"];

            if (!string.IsNullOrEmpty(jwtString))
            {
                var handler = new JwtSecurityTokenHandler();

                // Define your secret key (ensure it matches the key used for token generation)
                var securityKey =
                    "jDv3wF1oZTcX7rEm5qHlA4N8kGyS9iP2uWbO6sYtLxKzJgRnU0fDpVQeCbIaMh";

                // Define token validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // Set to true if you need issuer validation
                    ValidateIssuerSigningKey = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))

                    // Set the ValidAudience property to the expected audience value
                };

                try
                {
                    // Validate the token
                    handler.ValidateToken(jwtString, tokenValidationParameters, out _);
                    // Token is valid
                    return true;
                }
                catch (SecurityTokenException error) { 
                

                    return false;
                }
            }

            return false;
        }
    }
}
