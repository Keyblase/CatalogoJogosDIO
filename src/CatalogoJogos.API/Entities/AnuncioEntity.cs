using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.API.Entities
{
    public class AnuncioEntity
    {
        public Guid Id { get; set; }

        public string IdJogo { get; set; }
        public string Titulo { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public string Descricao { get; set; }
        public int QtdeCurtidas { get; set; }
        public int QtdeComentarios { get; set; }

        public AnuncioEntity()
        {
            DataDeCriacao = DateTime.Now;
            QtdeComentarios = 0;
            QtdeCurtidas = 0;
        }
    }
}
