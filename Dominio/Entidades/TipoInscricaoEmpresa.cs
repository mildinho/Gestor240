﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{

    [Index(nameof(Codigo),IsUnique = true)]
    [Index(nameof(Descricao))]
    public class TipoInscricaoEmpresa : ModelBase
    {

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Range(1, 9,ErrorMessage = "Valor Deve Entre 1 ~ 999")]
        public int Codigo { get; set; } 


        [Display(Name = "Tipo de Inscrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Descricao { get; set; } = String.Empty;

    }
}
