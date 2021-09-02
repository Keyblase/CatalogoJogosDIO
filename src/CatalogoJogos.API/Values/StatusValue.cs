using System.ComponentModel.DataAnnotations;

namespace CatalogoJogos.API.Values
{
    public enum StatusValue
    {
        [Display(Name = "Extremamente Positivas")]
        ExtremamentePositivas = 0,

        [Display(Name = "Ligeiramente Positivas")]
        LigeiramentePositivas,

        [Display(Name = "Positivas")]
        Positivas,

        [Display(Name = "Neutras")]
        Neutras,

        [Display(Name = "Negativas")]
        Negativas,

        [Display(Name = "Ligeiramente Negativas")]
        LigeiramenteNegativas,

        [Display(Name = "Extremamente Negativas")]
        ExtremamenteNegativas
    }
}
