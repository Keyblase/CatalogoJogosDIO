using CatalogoJogos.API.Exceptions;
using CatalogoJogos.API.InputModel;
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
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de anuncio</response>
        /// <response code="204">Caso não haja anuncios</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnuncioViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var anuncios = await _anuncioService.Obter(pagina, quantidade);

            if (anuncios.Count() == 0)
                return NoContent();

            return Ok(anuncios);
        }

        /// <summary>
        /// Buscar um anuncio pelo seu Id
        /// </summary>
        /// <param name="idAnuncio">Id do anuncio buscado</param>
        /// <response code="200">Retorna o anuncio filtrado</response>
        /// <response code="204">Caso não haja anuncio com este id</response>   
        [HttpGet("{idAnuncio:Guid}")]
        public async Task<ActionResult<AnuncioViewModel>> Obter([FromRoute] Guid idAnuncio)
        {
            var anuncio = await _anuncioService.Obter(idAnuncio);

            if (anuncio == null)
                return NoContent();

            return Ok(anuncio);
        }

        /// <summary>
        /// Inserir um anuncio no catálogo
        /// </summary>
        /// <param name="anuncioModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o anuncio seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um anuncio com mesmo titulo</response>   
        [HttpPost]
        public async Task<ActionResult<AnuncioViewModel>> InserirAnuncio([FromBody] AnuncioModel anuncioModel)
        {
            try
            {
                var jogo = await _anuncioService.Inserir(anuncioModel);

                return Ok(jogo);
            }
            catch (AnuncioJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um anuncio com este nome para esta produtora");
            }
        }

        /// <summary>
        /// Atualizar um anuncio no catálogo
        /// </summary>
        /// /// <param name="idAnuncio">Id do anuncio a ser atualizado</param>
        /// <param name="anuncioModel">Novos dados para atualizar o anuncio indicado</param>
        /// <response code="200">Cao o anuncio seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um anuncio com este Id</response>   
        [HttpPut("{idAnuncio:Guid}")]
        public async Task<ActionResult> AtualizarAnuncio([FromRoute] Guid idAnuncio, [FromBody] AnuncioModel anuncioModel)
        {
            try
            {
                await _anuncioService.Atualizar(idAnuncio, anuncioModel);

                return Ok();
            }
            catch (AnuncioNaoCadastradoException ex)
            {
                return NotFound("Não existe este anuncio");
            }
        }

        /// <summary>
        /// Atualizar o titulo de um anuncio
        /// </summary>
        /// /// <param name="idAnuncio">Id do anuncio a ser atualizado</param>
        /// <param name="titulo">Novo Titulo do anuncio</param>
        /// <response code="200">Caso o titulo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um anuncio com este Id</response>   
        [HttpPatch("{idAnuncio:Guid}/titulo/{titulo}")]
        public async Task<ActionResult> AtualizarTituloAnuncio([FromRoute] Guid idAnuncio, [FromRoute] string titulo)
        {
            try
            {
                await _anuncioService.Atualizar(idAnuncio, titulo);

                return Ok();
            }
            catch (AnuncioNaoCadastradoException ex)
            {
                return NotFound("Não existe este anuncio");
            }
        }

        /// <summary>
        /// Atualizar a descrição de um anuncio
        /// </summary>
        /// /// <param name="idAnuncio">Id do anuncio a ser atualizado</param>
        /// <param name="descricao">Nova descrição do anuncio</param>
        /// <response code="200">Caso a descrição seja atualizada com sucesso</response>
        /// <response code="404">Caso não exista um anuncio com este Id</response>   
        [HttpPatch("{idAnuncio:Guid}/descricao/{descricao}")]
        public async Task<ActionResult> AtualizarDescricaoAnuncio([FromRoute] Guid idAnuncio, [FromRoute] string descricao)
        {
            try
            {
                await _anuncioService.Atualizar(idAnuncio, descricao);

                return Ok();
            }
            catch (AnuncioNaoCadastradoException ex)
            {
                return NotFound("Não existe este anuncio");
            }
        }

        /// <summary>
        /// Excluir um anuncio
        /// </summary>
        /// /// <param name="idAnuncio">Id do anuncio</param>
        /// <response code="200">Caso a exclusao seja feita com sucesso</response>
        /// <response code="404">Caso não exista um anuncio com este Id</response>   
        [HttpDelete("{idAnuncio:Guid}")]
        public async Task<ActionResult> ApagarAnuncio([FromRoute] Guid idAnuncio)
        {
            try
            {
                await _anuncioService.Remover(idAnuncio);

                return Ok();
            }
            catch (AnuncioNaoCadastradoException ex)
            {
                return NotFound("Não existe este anuncio");
            }
        }

        /// <summary>
        /// Adicionar comentarios do anuncio
        /// </summary>
        /// /// <param name="idAnuncio">Id do anuncio a ser excluído</param>
        /// <response code="200">Caso a adicao de comentario seja feita com sucesso</response>
        /// <response code="404">Caso não exista um anuncio com este Id</response>  
        [HttpPatch("{idAnuncio:Guid}/comentario/adicionar")]
        public async Task<ActionResult> AdicionarComentario([FromRoute] Guid idAnuncio)
        {
            try
            {
                await _anuncioService.AdicionarComentario(idAnuncio);

                return Ok();
            }
            catch (AnuncioNaoCadastradoException ex)
            {
                return NotFound("Não existe este anuncio");
            }
        }

        /// <summary>
        /// Adicionar curtidas do anuncio
        /// </summary>
        /// /// <param name="idAnuncio">Id do anuncio a ser excluído</param>
        /// <response code="200">Caso a adicao de curtida seja feita com sucesso</response>
        /// <response code="404">Caso não exista um anuncio com este Id</response>  
        [HttpPatch("{idAnuncio:Guid}/curtidas/adicionar")]
        public async Task<ActionResult> AdicionarCurtidas([FromRoute] Guid idAnuncio)
        {
            try
            {
                await _anuncioService.AdicionarCurtida(idAnuncio);

                return Ok();
            }
            catch (AnuncioNaoCadastradoException ex)
            {
                return NotFound("Não existe este anuncio");
            }
        }
    }
}
