using Bogus;
using CatalogoJogos.API.Entities;
using CatalogoJogos.API.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        public static readonly List<string> idiomas = new List<string> {"portuges", "ingles", "coreano", "japones", "mandarim"};
        public static readonly List<string> generos = new List<string> { "açao", "aventura", "RPG", "Shooter", "Plataforma" };

        static List<AnuncioEntity> fakerAnuncioEntity = new Faker<AnuncioEntity>("pt_BR")
                .RuleFor(c => c.Titulo, f => f.Name.JobTitle())
                .RuleFor(c => c.Descricao, f => f.Lorem.Lines(3))
                .Generate(6);

        static List<JogoEntity> fakerJogoEntity = new Faker<JogoEntity>("pt_BR")
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.Nome, f => f.Name.JobTitle())
                .RuleFor(c => c.Distribuidora, f => f.Name.JobArea())
                .RuleFor(c => c.Produtora, f => f.Name.JobArea())
                .RuleFor(c => c.DataDeLancamento, f => f.Date.Past())
                .RuleFor(c => c.Descricao, f => f.Lorem.Lines(3))
                .RuleFor(c => c.Score, f => f.Random.Double(1, 100).ToString())
                .RuleFor(c => c.Status, f => f.PickRandom<StatusValue>())
                .RuleFor(c => c.Classificacao, f => f.PickRandom<ClassificacaoValue>())
                .RuleFor(c => c.Preco, f => f.Random.Double(1, 1000))
                .RuleFor(c => c.Idiomas, f => new List<string> { f.PickRandom(idiomas), f.PickRandom(idiomas) })
                .RuleFor(c => c.Generos, f => new List<string> { f.PickRandom(generos), f.PickRandom(generos) })
                .RuleFor(c => c.Anuncios, f => new List<AnuncioEntity> { f.PickRandom(fakerAnuncioEntity) })
                .Generate(6);

        private static Dictionary<Guid, JogoEntity> jogos = new Dictionary<Guid, JogoEntity>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), fakerJogoEntity[0]},
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), fakerJogoEntity[1]},
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), fakerJogoEntity[2]},
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), fakerJogoEntity[3]},
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), fakerJogoEntity[4]},
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), fakerJogoEntity[5]}
        };

        public Task<List<JogoEntity>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<JogoEntity> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return Task.FromResult<JogoEntity>(null);

            return Task.FromResult(jogos[id]);
        }

        public Task<List<JogoEntity>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<JogoEntity>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<JogoEntity>();

            foreach(var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(JogoEntity jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(JogoEntity jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
