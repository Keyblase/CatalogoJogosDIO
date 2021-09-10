using CatalogoJogos.API.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatalogoJogos.API.InputModel
{
    public class JogoModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string Produtora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000 reais")]
        public double Preco { get; set; }

        //Adição
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da distribuidora  deve conter entre 1 e 100 caracteres")]
        public string Distribuidora { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataDeLancamento { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        
        [Required]
        [Range(1, 100, ErrorMessage = "O Score deve ser de no mínimo 1  e no máximo 100")]
        public string Score { get; set; }

        //enumeradores
        public IEnumerable<SelectListItem> StatusValue { get; set; }
        public IEnumerable<SelectListItem> ClassificacaoValue { get; set; }

        //Lista itens
        public IEnumerable<string> Idiomas { get; set; }
        public IEnumerable<string> Generos { get; set; }
    }
}
