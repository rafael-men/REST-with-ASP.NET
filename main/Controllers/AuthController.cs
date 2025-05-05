using main.Business;
using main.Models;
using main.VO;
using Microsoft.AspNetCore.Mvc;

namespace main.Controllers
{
    [Route("v1/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginVO userCredentials)
        {
            if (userCredentials == null || string.IsNullOrWhiteSpace(userCredentials.UserName) || string.IsNullOrWhiteSpace(userCredentials.Password))
            {
                return BadRequest("Credenciais Inválidas");
            }

         
            var token = _loginBusiness.ValidateCredentials(userCredentials);
            if (token == null)
            {
                return Unauthorized("Token Inválido");
            }

            return Ok(new { token });
        }

       
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserVO user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Usuário e senha são obrigatórios.");
            }

            var createdUser = _loginBusiness.RegisterUser(user);
            if (createdUser == null)
            {
                return Conflict("Nome de usuário já está em uso.");
            }

            return Created("", new { message = "Usuário registrado com sucesso!" });
        }
    }
}
