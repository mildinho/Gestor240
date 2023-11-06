using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{

    [Index(nameof(Nome))]
    [Index(nameof(UFId), nameof(Nome), IsUnique = true)]

    public class Municipio : ModelBase
    {

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        [ForeignKey("UF")]
        public int UFId { get; set; }
        public virtual UF? UF { get; set; }


        [Display(Name = "Nome")]
        [StringLength(100)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)] 
        public string Nome { get; set; } = String.Empty;



        [Display(Name = "Código Fiscal")]
        [StringLength(07)]
        public string CodigoFiscal { get; set; } = String.Empty;
    }
}
