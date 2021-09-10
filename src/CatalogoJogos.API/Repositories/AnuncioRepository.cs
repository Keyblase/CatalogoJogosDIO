using Bogus;
using CatalogoJogos.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Repositories
{
    public class AnuncioRepository : IAnuncioRepository
    {
        static List<AnuncioEntity> fakerAnuncioEntity = new Faker<AnuncioEntity>("pt_BR")
                .RuleFor(c => c.Titulo, f => f.Name.JobTitle())
                .RuleFor(c => c.Descricao, f => f.Lorem.Lines(3))
                .RuleFor(c => c.QtdeComentarios, f => f.Random.Number(1,1000))
                .RuleFor(c => c.QtdeCurtidas, f => f.Random.Number(1, 1000))
                .Generate(6);

        private static Dictionary<Guid, AnuncioEntity> anuncios = new Dictionary<Guid, AnuncioEntity>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), fakerAnuncioEntity[0]},
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), fakerAnuncioEntity[1]},
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), fakerAnuncioEntity[2]},
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), fakerAnuncioEntity[3]},
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), fakerAnuncioEntity[4]},
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), fakerAnuncioEntity[5]}
        };


        public Task Atualizar(AnuncioEntity anuncio)
        {
            anuncios[anuncio.Id] = anuncio;
            return Task.CompletedTask;
        }

        public Task Inserir(AnuncioEntity anuncio)
        {
            anuncios.Add(anuncio.Id, anuncio);
            return Task.CompletedTask;
        }

        public Task<List<AnuncioEntity>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(anuncios.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<AnuncioEntity> Obter(Guid id)
        {
            if (!anuncios.ContainsKey(id))
                return Task.FromResult<AnuncioEntity>(null);

            return Task.FromResult(anuncios[id]);
        }

        public Task Remover(Guid id)
        {
            anuncios.Remove(id);
            return Task.CompletedTask;
        }

        public Task<List<AnuncioEntity>> Obter(string titulo)
        {
            return Task.FromResult(anuncios.Values.Where(anuncio => anuncio.Titulo.Equals(titulo)).ToList());
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }

        public Task AdicionarComentario(Guid id)
        {
            anuncios[id].QtdeComentarios += 1;
            return Task.CompletedTask;
        }

        public Task AdicionarCurtida(Guid id)
        {
            anuncios[id].QtdeComentarios += 1;
            return Task.CompletedTask;
        }
    }
}
