using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dominio.Entidades
{
    public class Agencia : ModelBase
    {

        
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        [ForeignKey("Banco")]
        public int BancoId { get; set; }
        public virtual Banco Banco { get; set; }



        [Display(Name = "Número da Agência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(5)]
        public string NumeroAgencia { get; set; } = String.Empty;

        [Display(Name = "Dígito da Agência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string DigitoAgencia { get; set; } = String.Empty;




        [Display(Name = "Número da Conta Corrente")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(12)]
        public string NumeroConta { get; set; } = String.Empty;

        [Display(Name = "Dígito da Conta Corrente")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string DigitoConta { get; set;} = String.Empty;




        [Display(Name = "Número do Convênio")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(20)]
        public string NumeroConvenio { get; set; } = String.Empty;


    }
}
