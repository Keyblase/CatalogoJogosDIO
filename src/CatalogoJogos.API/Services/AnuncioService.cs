using CatalogoJogos.API.Entities;
using CatalogoJogos.API.InputModel;
using CatalogoJogos.API.Repositories;
using CatalogoJogos.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Services
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioService(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }

        public async Task Atualizar(Guid id, AnuncioModel anuncio)
        {
            var entidadeAnuncio = await _anuncioRepository.Obter(id);

            if (entidadeAnuncio == null)
                throw new Exception("Anuncio nao cadastrado");

            entidadeAnuncio.Descricao = anuncio.Descricao;
            entidadeAnuncio.Titulo = anuncio.Titulo;
            entidadeAnuncio.IdJogo = anuncio.IdJogo;

            await _anuncioRepository.Atualizar(entidadeAnuncio);
        }

        public async Task Atualizar(Guid id, string titulo)
        {
            var entidadeAnuncio = await _anuncioRepository.Obter(id);

            if (entidadeAnuncio == null)
                throw new Exception("Anuncio nao cadastrado");

            entidadeAnuncio.Titulo = titulo;

            await _anuncioRepository.Atualizar(entidadeAnuncio);
        }

        public async Task<AnuncioViewModel> Inserir(AnuncioModel anuncio)
        {
            var entidadeAnuncio = await _anuncioRepository.Obter(anuncio.Titulo);

            if (entidadeAnuncio.Count > 0)
                throw new Exception("Anuncio Ja criado");

            var anuncioInsercao = new AnuncioEntity
            {
                Id = Guid.NewGuid(),
                Descricao = anuncio.Descricao,
                Titulo = anuncio.Titulo
            };

            await _anuncioRepository.Inserir(anuncioInsercao);

            return new AnuncioViewModel
            {
               Anuncio = anuncioInsercao
            };
        }

        public async Task<List<AnuncioViewModel>> Obter(int pagina, int quantidade)
        {
            var anuncios = await _anuncioRepository.Obter(pagina, quantidade);

            return anuncios.Select(anuncio => new AnuncioViewModel
            {
                Anuncio = anuncio
            }).ToList(); ;
        }

        public async Task<AnuncioViewModel> Obter(Guid id)
        {
            var anuncio = await _anuncioRepository.Obter(id);

            if (anuncio == null)
                return null;

            return new AnuncioViewModel
            {
                Anuncio = anuncio
            };
        }

        public async Task Remover(Guid id)
        {
            var anuncio = await _anuncioRepository.Obter(id);

            if (anuncio == null)
                throw new Exception("Anuncio nao cadastrado");

            await _anuncioRepository.Remover(id);
        }

        public async Task AdicionarComentario(Guid id)
        {
            var anuncio = await _anuncioRepository.Obter(id);

            if (anuncio == null)
                throw new Exception("Anuncio nao cadastrado");

            await _anuncioRepository.AdicionarComentario(id);
        }

        public async Task AdicionarCurtida(Guid id)
        {
            var anuncio = await _anuncioRepository.Obter(id);

            if (anuncio == null)
                throw new Exception("Anuncio nao cadastrado");

            await _anuncioRepository.AdicionarCurtida(id);
        }

        public void Dispose()
        {
            //para bd
        }
    }
}
