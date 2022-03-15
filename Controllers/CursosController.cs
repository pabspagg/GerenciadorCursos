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
    public class CursosController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CursosController(IUnitOfWork uow) => _uow = uow;

        /// <summary>
        /// Retorna todas as cursos.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var resultado = (await _uow.Cursos.FindAllAsync(false))
                    .Select(
                        e =>
                            new {
                                e.Id,
                                e.Titulo,
                                e.Duracao,
                                Status = Enum.GetName(e.Status),
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
        /// Retorna curso por id.
        /// </summary>
        /// <response code="200">Retorna curso cadastrado pelo id.</response>
        /// <response code="404">Curso não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var resultado = (await _uow.Cursos.FindByConditionAsync(e => e.Id == id, false)).Select(
                        e =>
                            new {
                                e.Id,
                                e.Titulo,
                                e.Duracao,
                                Status = Enum.GetName(e.Status),
                            }
                    );

                return resultado == null ? NotFound("Curso não encontrado.") : Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        /// <summary>
        /// Pesquisa curso por status.
        /// </summary>
        [HttpGet]
        [Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByStatus([FromQuery] Status query)
        {
            try
            {
                var resultado = (await _uow.Cursos.FindAllAsync(false))
                    .Where(e => e.Status == query)
                    .Select(e =>
                          new {
                              Id = e.Id,
                              Duracao = e.Duracao,
                              Titulo = e.Titulo,
                              Status = Enum.GetName(e.Status),
                          })
                    .ToList();

                return resultado == null ? NotFound("Curso não encontrado.") : Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        /// <summary>
        /// Adiciona um único curso.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Gerencia,Secretaria")]
        public async Task<IActionResult> PostAsync([FromBody] CursoModelAddDto model)
        {

            try
            {
                // Verifica se já existe curso
                var resultado = (await _uow.Cursos.FindByConditionAsync(e => e.Titulo == model.Titulo, false))
                    .FirstOrDefault();

                if (resultado != null) return BadRequest("Não é possível adicionar cursos com mesmo nome.");

                // Cria nova curso a ser adicionado
                var modelo = new CursoModel(
                    model.Titulo,
                    model.Duracao,
                    model.Status
                );
                //Adiciona curso e salva
                await _uow.Cursos.CreateAsync(modelo);
                await _uow.Commit();
                return Ok(modelo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Atualiza curso.
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Gerencia,Secretaria")]
        public async Task<IActionResult> PutAsync([FromBody] CursoModelUpdateDto model, [FromRoute] int id)
        {
            try
            {
                var resultado = (await _uow.Cursos.FindByConditionAsync(e => e.Id == id, false)).FirstOrDefault();
                if (resultado == null) return NotFound("Curso não encontrado.");

                resultado.Status = model.Status;
                await _uow.Cursos.UpdateAsync(resultado);
                await _uow.Commit();
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        /// <summary>
        /// Deleta curso por id.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Gerencia")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var resultado = (await _uow.Cursos.FindByConditionAsync(e => e.Id == id, false)).FirstOrDefault();
                if (resultado == null) return NotFound("Curso não encontrado.");

                await _uow.Cursos.RemoveAsync(resultado);
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