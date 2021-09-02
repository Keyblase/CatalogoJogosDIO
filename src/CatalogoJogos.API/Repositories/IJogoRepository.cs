using CatalogoJogos.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Repositories
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<JogoEntity>> Obter(int pagina, int quantidade);
        Task<JogoEntity> Obter(Guid id);
        Task<List<JogoEntity>> Obter(string nome, string produtora);
        Task Inserir(JogoEntity jogo);
        Task Atualizar(JogoEntity jogo);
        Task Remover(Guid id);
    }
}
