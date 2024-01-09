using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Dominio.DTO
{
    public class LoginRegistroDTO
    {
        public int Id { get; set; } = 0;

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
        [Compare("Password", ErrorMessage = "Senha NÃO Confere")]
        public string ConfirmPassword { get; set; } = String.Empty;

        public bool SexoMasculino { get; set; } = true;

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; } = DateTime.Now;


        public LoginRegistroDTO() { }


        public static Login ToEntidade(LoginRegistroDTO loginDTO)
        {
            return new Login
            {
                Id = loginDTO.Id,
                Email = loginDTO.Email,
                Password = loginDTO.Password,
                Nome = loginDTO.Nome,
                DataNascimento = loginDTO.DataNascimento,
                SexoMasculino = loginDTO.SexoMasculino
            };

        }


        public static IEnumerable<LoginRegistroDTO> ToDTO(IEnumerable<Login> login)
        {
            List<LoginRegistroDTO> loginDTO = new();

            foreach (var item in login)
            {
                loginDTO.Add(new LoginRegistroDTO
                {
                    Id = item.Id,
                    Email = item.Email,
                    Password = item.Password,
                    Nome = item.Nome,
                    DataNascimento = item.DataNascimento,
                    SexoMasculino = item.SexoMasculino
                });
            }
            return loginDTO;

        }

        public static LoginRegistroDTO ToDTO(Login login)
        {
            return new LoginRegistroDTO
            {
                Id=login.Id,
                Email = login.Email,
                Password = login.Password,
                Nome = login.Nome,
                DataNascimento = login.DataNascimento,
                SexoMasculino = login.SexoMasculino

            };
        }


    }
}
