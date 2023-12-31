﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{

    [Index(nameof(Codigo),IsUnique = true)]
    [Index(nameof(Nome))]
    public class Banco : ModelBase
    {

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Range(1, 999,ErrorMessage = "Valor Deve Entre 1 ~ 999")]
        public int Codigo { get; set; } 


        [Display(Name = "Nome do Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Nome { get; set; } = String.Empty;

        [Display(Name = "Código ISPB ")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(10)]
        public string ISPB { get; set; } = String.Empty;


          

    }
}
