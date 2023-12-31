﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{

    [Index(nameof(Sigla), IsUnique = true)]
    [Index(nameof(Descricao))]
    public class UF : ModelBase
    {
        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(2)]
        public string Sigla { get; set; } = String.Empty;

        [Display(Name = "Descrição")]
        [StringLength(50)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)] 
        public string Descricao { get; set; } = String.Empty;



        [Display(Name = "Código Fiscal")]
        [StringLength(02)]
        public string CodigoFiscal { get; set; } = String.Empty;
    }
}
