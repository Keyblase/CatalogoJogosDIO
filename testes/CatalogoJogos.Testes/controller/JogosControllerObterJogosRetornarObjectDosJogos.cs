using CatalogoJogos.API.Controllers.V2;
using CatalogoJogos.API.InputModel;
using CatalogoJogos.API.Repositories;
using CatalogoJogos.API.Services;
using CatalogoJogos.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CatalogoJogos.Testes.controller
{
    public class JogosControllerObterJogosRetornarObjectDosJogos
    {
        IJogoRepository _jogoRepository;
        JogoService _jogoService;
        JogosController _jogosController;

        public JogosControllerObterJogosRetornarObjectDosJogos()
        {
            _jogoRepository = Substitute.For<IJogoRepository>();
            _jogoService = new JogoService(_jogoRepository);
            _jogosController = new JogosController(_jogoService);
        }

        #region testes para dar como correto
        [Theory]
        [InlineData(4,5)]
        [InlineData(1,6)]
        public void Get_QuandoChamado_ReturnaListaDeJogosObjectResult(int pagina,int quantidade)
        {
            // Act
            var okResult = _jogosController.Obter(pagina,quantidade);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Theory]
        [InlineData("0ca314a5-9282-45d8-92c3-2985f2a9fd04")]
        [InlineData("92576bd2-388e-4f5d-96c1-8bfda6c5a268")]
        public void Get_QuandoChamado_ReturnaAsInformacoesJogoId(Guid id)
        {
            // Act
            var okResult = _jogosController.Obter(id);
            // Assert
            var item = Assert.IsType<JogoViewModel>(okResult.Result.Value);
            Assert.NotNull(item);
        }
        #endregion

        #region testes para dar excessoes
        [Theory]
        [InlineData("2baf70d1-42bb-4437-b551-e5fed5a87abe")]
        [InlineData("12cfb892-aac0-4c5b-94af-521852e46d6a")]
        public void Get_QuandoChamado_ReturnaOkResultMensagemExcessao(Guid id)
        {
            // Act
            var notContent = _jogosController.Obter(id);
            // Assert
            Assert.Equal(StatusCodes.Status204NoContent, notContent.Status.GetHashCode());
            Assert.IsType<NoContentResult>(notContent.Result);
        }
        #endregion
    }
}

