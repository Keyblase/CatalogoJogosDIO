using System;

namespace CatalogoJogos.API.Exceptions
{
    public class AnuncioJaCadastradoException : Exception
    {
        public AnuncioJaCadastradoException()
            : base("Este já anuncio está cadastrado")
        { }
    }
}
