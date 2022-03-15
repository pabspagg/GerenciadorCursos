using GerenciadorCursos.DataAcessRepo.UnitOfWork;
using GerenciadorCursos.DomainCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public LoginController(IConfiguration config, IUnitOfWork uow)
        {
            _config = config;
            _uow = uow;
        }

        /// <summary>
        ///  Login do usuário.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserModelLoginDto model)
        {
            try
            {
                var resultado = (await _uow.Users.FindByConditionAsync(e => model.Username == e.Username, false)).FirstOrDefault();

                if (resultado == null) return (new { message = "Usuário ou senha inválidos" });

                var token = GenerateToken(resultado);
                return Ok(new { token });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        private string GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                         new Claim(ClaimTypes.Name, user.Username.ToString()),
                         new Claim(ClaimTypes.Role, user.Role.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(
                    Convert.ToDouble(_config.GetValue<string>("JwtToken:TokenExpiry"))
                ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(_config.GetValue<string>("JwtToken:SecretKey"))
                    ),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}