using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
  
   
    public class Agencia : ModelBase
    {

        
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Banco")]
        [ForeignKey("Banco")]
        public int BancoId { get; set; }
        public virtual Banco? Banco { get; set; }



        [Display(Name = "Número da Agência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public int NumeroAgencia { get; set; } = 0;

        [Display(Name = "Dígito da Agência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string DigitoAgencia { get; set; } = String.Empty;


        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Nome { get; set; } = String.Empty;





    }
}
