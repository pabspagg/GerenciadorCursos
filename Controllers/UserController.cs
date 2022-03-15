using GerenciadorCursos.DataAcessRepo.UnitOfWork;
using GerenciadorCursos.DomainCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCursos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UserController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        ///  Exibe todos usuários.
        ///  </summary>
        [HttpGet]
        [AllowAnonymous]
        [Authorize("Gerencia")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var resultado = (await _uow.Users.FindAllAsync(false))
                    .Select(
                        e =>
                            new {
                                e.Id,
                                e.Username,
                                e.Password,
                                Status = Enum.GetName(e.Role),
                            }
                    )
                    .ToList();

                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        ///  Cadastro usuário.
        /// </summary>
        [HttpPost]
        [Route("cadastro")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] UserModelDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Modelo inválido. Não é permitido campos em branco.");

            var modelo = new UserModel(model.Username, model.Password, model.Role);

            try
            {
                var usuarios = (await _uow.Users.FindByConditionAsync(i => i.Username == model.Username, false))
                    .FirstOrDefault();

                if (usuarios != null) return BadRequest("Não é possível adicionar usuário existente.");

                await _uow.Users.CreateAsync(modelo);
                await _uow.Commit();
                return Ok(modelo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        ///  Procura usuário pelo id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [Authorize("Gerencia")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var resultado = (await _uow.Users.FindByConditionAsync(e => e.Id == id, false));

                if (resultado == null)
                    return NotFound();
                return resultado == null ? NotFound("Usuário não encontrado.") : Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Atualiza usuário.
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [Authorize("Gerencia")]
        public async Task<IActionResult> PutAsync([FromBody] UserModelDto model, [FromRoute] int id)
        {
            //Verifica se o Modelo é válido
            if (!ModelState.IsValid) return BadRequest("Modelo inválido");

            try
            {
                var resultado = (await _uow.Users.FindByConditionAsync(e => e.Id == id, false)).FirstOrDefault();
                if (resultado == null) return NotFound();

                resultado.Username = model.Username;
                resultado.Password = model.Password;
                resultado.Role = model.Role;

                await _uow.Users.UpdateAsync(resultado);
                await _uow.Commit();
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Deleta usuário por id.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize("Gerencia")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var resultado = (await _uow.Users.FindByConditionAsync(e => e.Id == id, false)).FirstOrDefault();
                if (resultado == null) return NotFound("Usuário não encontrado.");

                await _uow.Users.RemoveAsync(resultado);
                await _uow.Commit();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
    }
}