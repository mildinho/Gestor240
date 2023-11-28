using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Dominio.DTO
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "EMail Inválido")]
        public string Email { get; set; } = String.Empty;


        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0} Deve haver no Minimo {2} e no Máximo {1}", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;



        public LoginDTO() { }




        public static Login ToEntidade(LoginDTO loginDTO)
        {
            return new Login
            {
                Email = loginDTO.Email,
                Password = loginDTO.Password
            };

        }

        public static IEnumerable<LoginDTO> ToDTO(IEnumerable<Login> login)
        {
            List<LoginDTO> loginDTO = new();

            foreach (var item in login)
            {
                loginDTO.Add(new LoginDTO
                {
                    Email = item.Email,
                    Password = item.Password
                });
            }
            return loginDTO;

        }

        public static LoginDTO ToDTO(Login login)
        {
            return new LoginDTO
            {
                Email = login.Email,
                Password = login.Password

            };
        }
    }
}
