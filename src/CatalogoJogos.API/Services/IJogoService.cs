using CatalogoJogos.API.InputModel;
using CatalogoJogos.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid id);
        Task<JogoViewModel> Inserir(JogoModel jogo);
        Task Atualizar(Guid id, JogoModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
