using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{

    [Index(nameof(Email), IsUnique = true)]

    public class Login : ModelBase
    {

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "EMail Inválido")]
        public string Email { get; set; } = String.Empty;


        public string Nome { get; set; } = String.Empty;


        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0} Deve haver no Minimo {2} e no Máximo {1}", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

    }

}
