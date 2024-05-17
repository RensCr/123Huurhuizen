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
                var securityKey =
                    "jDv3wF1oZTcX7rEm5qHlA4N8kGyS9iP2uWbO6sYtLxKzJgRnU0fDpVQeCbIaMh";

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                };

                try
                {
                    handler.ValidateToken(jwtString, tokenValidationParameters, out _);
                    return true;
                }
                catch (SecurityTokenException error) { 
                

                    return false;
                }
            }

            return false;
        }

        public int GetUserId(HttpRequest request)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtTokenCookie = request.Cookies["jwtToken"];

                if (jwtTokenCookie != null)
                {
                    var tokenString = jwtTokenCookie;
                    var token = tokenHandler.ReadJwtToken(tokenString);

                    var IdClaim = token.Claims.FirstOrDefault(c => c.Type == "Id");

                    if (IdClaim != null)
                    {
                        var Id = int.Parse(IdClaim.Value);
                        return Id;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
