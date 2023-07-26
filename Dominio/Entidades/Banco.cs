using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{

    [Index(nameof(Codigo))]
    
    public class Banco : ModelBase
    {
        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Range(1, 999)]
        [StringLength(3)]
        public string Codigo { get; set; } = String.Empty;


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
