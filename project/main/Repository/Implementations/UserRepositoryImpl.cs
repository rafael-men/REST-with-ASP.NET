using System.Data;
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
            var password = user.Password?.ToString();
            var pass = ComputeHash(password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => ((u.UserName == user.UserName) && (u.Password == pass)));
        }

        private object ComputeHash(string input, SHA256CryptoServiceProvider provider)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = provider.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }


        public bool ExistsByUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User FindByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

    }
}
