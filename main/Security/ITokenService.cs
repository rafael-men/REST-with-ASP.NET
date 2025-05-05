using System.Security.Claims;
using main.Models;

namespace main.Security
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
