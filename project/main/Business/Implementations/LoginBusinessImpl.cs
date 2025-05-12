using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using main.Models;
using main.Repository;
using main.Security;
using main.VO;
using Microsoft.Extensions.Options;

namespace main.Business.Implementations
{
    public class LoginBusinessImpl : ILoginBusiness
    {

        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImpl(IUserRepository userRepository, ITokenService tokenService)
        {

            _userRepository = userRepository;
            _tokenService = tokenService;
        }


        public string LoginUser(LoginVO login)
        {
            var user = _userRepository.FindByUserName(login.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                throw new Exception("Credenciais Inválidas");
            }

            return _tokenService.GenerateToken(user);
        }



        public User RegisterUser(UserVO userCredentials)
        {
            if (_userRepository.ExistsByUserName(userCredentials.UserName))
                return null;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCredentials.Password);

            var user = new User
            {
                UserName = userCredentials.UserName,
                Password = hashedPassword,
                FullName = userCredentials.FullName
            };

            return _userRepository.Create(user);
        }

    }
}
