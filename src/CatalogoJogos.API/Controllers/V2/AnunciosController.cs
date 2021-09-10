using CatalogoJogos.API.Services;
using CatalogoJogos.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AnunciosController : ControllerBase
    {
        private readonly IAnuncioService _anuncioService;

        public AnunciosController(IAnuncioService anuncioService)
        {
            _anuncioService = anuncioService;
        }

        /// <summary>
        /// Buscar todos os anuncios de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os anuncios sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnuncioViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var anuncios = await _anuncioService.Obter(pagina, quantidade);

            if (anuncios.Count() == 0)
                return NoContent();

            return Ok(anuncios);
        }
    }
}
