using System.ComponentModel.DataAnnotations;

namespace CatalogoJogos.API.Values
{
    public enum ClassificacaoValue
    {
        [Display(Name = "Livre")]
        Livre = 0,

        [Display(Name = "12 anos")]
        DozeAnos,

        [Display(Name = "14 anos")]
        CatorzeAnos,

        [Display(Name = "16 anos")]
        DezesseisAnos,

        [Display(Name = "18 anos")]
        DezoitoAnos
    }
}
