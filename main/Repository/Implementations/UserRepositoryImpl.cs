using System.Security.Cryptography;
using System.Text;
using main.Data;
using main.Models;
using main.VO;

namespace main.Repository.Implementations
{
    public class UserRepositoryImpl : IUserRepository
    {

        private readonly PostgreSqlContext _context;

        public UserRepositoryImpl(PostgreSqlContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return null;
        }

        private object ComputeHash(string input, SHA256CryptoServiceProvider provider)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = provider.ComputeHash(inputBytes, provider);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
