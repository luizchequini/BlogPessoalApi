using Data.Entidades;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaApiController : Controller
    {
        private readonly ICategoria _iCategoria;

        public CategoriaApiController(ICategoria iCategoria)
        {
            _iCategoria = iCategoria;
        }

        [HttpGet("ListarCategorias")]
        public async Task<JsonResult> ListaCategorias()
        {
            return Json(await this._iCategoria.List());
        }

        [HttpGet("ListarCategoria/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Categoria categoria = await _iCategoria.GetEntityById(id);

            if (categoria == null)
            {
                return NotFound("Não foi possível encontrar o registro de Categoria!");
            }

            return Ok(categoria);
        }

        [HttpPost("CadastrarCategoria")]
        public async Task CadastrarCategoria([FromBody] Categoria categoria)
        {
            await Task.FromResult(this._iCategoria.Add(categoria));
        }

        [HttpPut("AtualizarCategoria/{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Categoria é nula!");
            }
            Categoria categoriaAtualizar = await _iCategoria.GetEntityById(id);
            if (categoriaAtualizar == null)
            {
                return NotFound("Categoria não foi encontrada!");
            }
            await _iCategoria.Update(categoria);
            return NoContent();
        }

        [HttpDelete("ExcluirCategoria/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Categoria categoria = await _iCategoria.GetEntityById(id);
            if (categoria == null)
            {
                return BadRequest("Cliente não encontrado!");
            }
            await _iCategoria.Delete(categoria);
            return NoContent();
        }
    }
}
