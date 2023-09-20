using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{

    [Index(nameof(Descricao))]
    public class Municipio : ModelBase
    {

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        [ForeignKey("UF")]
        public int UFId { get; set; }
        public virtual UF? UF { get; set; }


        [Display(Name = "Descrição")]
        [StringLength(100)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)] 
        public string Descricao { get; set; } = String.Empty;



        [Display(Name = "Código Fiscal")]
        [StringLength(07)]
        public string CodigoFiscal { get; set; } = String.Empty;
    }
}
