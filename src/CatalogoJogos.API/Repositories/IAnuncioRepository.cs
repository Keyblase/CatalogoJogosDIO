using CatalogoJogos.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Repositories
{
    public interface IAnuncioRepository : IDisposable
    {
        Task<List<AnuncioEntity>> Obter(int pagina, int quantidade);
        Task<List<AnuncioEntity>> Obter(string titulo);
        Task<AnuncioEntity> Obter(Guid id);
        Task Inserir(AnuncioEntity jogo);
        Task Atualizar(AnuncioEntity jogo);
        Task Remover(Guid id);
        Task AdicionarComentario(Guid id);
        Task AdicionarCurtida(Guid id);

    }
}
