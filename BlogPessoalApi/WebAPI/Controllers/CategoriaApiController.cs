using Data.Entidades;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CategoriaApiController : Controller
    {
        private readonly ICategoria _iCategoria;

        public CategoriaApiController(ICategoria iCategoria)
        {
            _iCategoria = iCategoria;
        }

        [HttpGet("/api/ListarCategorias")]
        public async Task<JsonResult> ListaCategorias()
        {
            return Json(await this._iCategoria.List());
        }

        [HttpPost("/api/CadastrarCategoria")]
        public async Task CadastrarCategoria([FromBody] Categoria categoria)
        {
            await Task.FromResult(this._iCategoria.Add(categoria));
        }
    }
}
