using System;

namespace CatalogoJogos.API.Exceptions
{
    public class AnuncioNaoCadastradoException: Exception
    {
        public AnuncioNaoCadastradoException()
            :base("Este anuncio não está cadastrado")
        {}
    }
}
