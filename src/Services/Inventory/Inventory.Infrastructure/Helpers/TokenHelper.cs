using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Inventory.Infrastructure.Helpers
{
    /// <summary>
    /// Helper class that uses the Nuget package "System.IdentityModel.Tokens.Jwt" to validate the token
    /// </summary>
    public static class TokenHelper
    {
        public static Tuple<string, string> ValidateJwtToken(string token, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Jwt:Key").Value);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "GoalSystems",
                    ValidateAudience = true,
                    ValidAudience = "inventory_manager",
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return new Tuple<string, string> (jwtToken.Claims.First(x => x.Type == "Environment").Value, jwtToken.Claims.First(x => x.Type == "sub").Value);
            }
            catch
            {
                // if validation fails then return null
                return null;
            }
        }
    }
}
