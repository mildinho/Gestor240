using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dominio.Entidades
{
   
    public class Banco : ModelBase
    {
        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Range(1, 999)]
        [StringLength(3)]
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
