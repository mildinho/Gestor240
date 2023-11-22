using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{

    [Index(nameof(Descricao))]
    public class TipoContaCorrente : ModelBase
    {

        [Display(Name = "Descrição da Conta Corrente")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Descricao { get; set; } = String.Empty;

    }
}
