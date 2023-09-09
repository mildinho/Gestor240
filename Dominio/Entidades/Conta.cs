using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
  
   
    public class Conta : ModelBase
    {

        
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Agencia")]
        [ForeignKey("Agencia")]
        public int AgenciaId { get; set; }
        public virtual Agencia? Agencia { get; set; }



        [Display(Name = "Número da Conta Corrente")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public int NumeroConta { get; set; } = 0;

        [Display(Name = "Dígito da Conta Corrente")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string DigitoConta { get; set;} = String.Empty;


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Beneficiario")]
        [ForeignKey("Beneficiario")]
        public int BeneficiarioID { get; set; }
        public virtual Beneficiario? Beneficiario { get; set; }



        [Display(Name = "Número do Convênio")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(20)]
        public string NumeroConvenio { get; set; } = String.Empty;

        public int Sequencia_NSA { get; set; } = 0;

    }
}
