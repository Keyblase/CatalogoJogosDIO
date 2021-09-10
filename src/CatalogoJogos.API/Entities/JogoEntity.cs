using CatalogoJogos.API.Values;
using System;
using System.Collections.Generic;

namespace CatalogoJogos.API.Entities
{
    public class JogoEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Preco { get; set; }

        //Adição
        public string Distribuidora { get; set; }
        public DateTime DataDeLancamento { get; set; }
        public string Score { get; set; }

        //Podera ser Editado
        public string Descricao { get; set; }


        //enumeradores
        public StatusValue Status { get; set; }
        public ClassificacaoValue Classificacao { get; set; }

        //Entidades
        public ICollection<AnuncioEntity> Anuncios { get; set; }

        //Lista itens
        public IEnumerable<string> Idiomas { get; set; }
        public IEnumerable<string> Generos { get; set; }

    }
}
