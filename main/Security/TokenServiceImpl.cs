using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using main.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace main.Security
{
    public class TokenServiceImpl : ITokenService

    {
        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes("MY_BIG_SECRET_HERE");
            var Skey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(Skey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                   issuer: "Mine",
                   audience: "Mine",
                   claims: claims,
                   expires: DateTime.UtcNow.AddHours(1),
                   signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
