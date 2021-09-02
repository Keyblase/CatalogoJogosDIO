using System.ComponentModel.DataAnnotations;

namespace CatalogoJogos.API.InputModel
{
    public class AnuncioModel
    {
        [Required]
        [RegularExpression(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$")]
        public string IdJogo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Titulo do anúncio deve conter entre 3 e 100 caracteres")]
        public string Titulo { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "A descrição da produtora deve conter entre 3 e 100 caracteres")]
        public string Descricao { get; set; }

    }
}
