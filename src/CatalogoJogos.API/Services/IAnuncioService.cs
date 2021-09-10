using CatalogoJogos.API.InputModel;
using CatalogoJogos.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Services
{
    public interface IAnuncioService : IDisposable
    {
        Task<List<AnuncioViewModel>> Obter(int pagina, int quantidade);
        Task<AnuncioViewModel> Obter(Guid id);
        Task<AnuncioViewModel> Inserir(AnuncioModel anuncio);
        Task Atualizar(Guid id, AnuncioModel anuncio);
        Task Atualizar(Guid id, string titulo);
        Task Remover(Guid id);
        Task AdicionarComentario(Guid id);
        Task AdicionarCurtida(Guid id);

    }
}
