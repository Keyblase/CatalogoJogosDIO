using CatalogoJogos.API.Entities;
using CatalogoJogos.API.Exceptions;
using CatalogoJogos.API.Helpers;
using CatalogoJogos.API.InputModel;
using CatalogoJogos.API.Repositories;
using CatalogoJogos.API.Values;
using CatalogoJogos.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
                                {
                                    Id = jogo.Id,
                                    Nome = jogo.Nome,
                                    Produtora = jogo.Produtora,
                                    Preco = jogo.Preco,
                                    DataDeLancamento = jogo.DataDeLancamento,
                                    Descricao = jogo.Descricao,
                                    Score = jogo.Score,
                                    Status = EnumHelper<StatusValue>.GetDisplayValue(jogo.Status),
                                    Classificacao = EnumHelper<ClassificacaoValue>.GetDisplayValue(jogo.Classificacao),
                                    Distribuidora = jogo.Distribuidora,
                                    Generos = jogo.Generos,
                                    Idiomas = jogo.Idiomas,
                                    Anuncios = jogo.Anuncios
                                })
                               .ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                DataDeLancamento = jogo.DataDeLancamento,
                Descricao = jogo.Descricao,
                Score = jogo.Score,
                Status = EnumHelper<StatusValue>.GetDisplayValue(jogo.Status),
                Classificacao = EnumHelper<ClassificacaoValue>.GetDisplayValue(jogo.Classificacao),
                Distribuidora = jogo.Distribuidora,
                Generos = jogo.Generos,
                Idiomas = jogo.Idiomas,
                Anuncios = jogo.Anuncios
            };
        }

        public async Task<JogoViewModel> Inserir(JogoModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new JogoEntity
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Atualizar(Guid id, JogoModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, string descricao)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Descricao = descricao;

            await _jogoRepository.Atualizar(entidadeJogo);
        }


        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }

    }
}
