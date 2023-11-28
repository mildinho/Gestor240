using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Dominio.DTO
{
    public class LoginRegistroDTO
    {

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "EMail Inválido")]
        public string Email { get; set; } = String.Empty;


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = String.Empty;


        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0} Deve haver no Minimo {2} e no Máximo {1}", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [Display(Name = "Senha Confirmação")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0} Deve haver no Minimo {2} e no Máximo {1}", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Senha Estão Diferentes")]
        public string ConfirmPassword { get; set; } = String.Empty;



        public LoginRegistroDTO() { }


        public static Login ToEntidade(LoginRegistroDTO loginDTO)
        {
            return new Login
            {
                Email = loginDTO.Email,
                Password = loginDTO.Password,
                Nome = loginDTO.Nome,   
            };

        }

        
    }
}
