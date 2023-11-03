using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class AgenciaDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Banco")]
        [ForeignKey("Banco")]
        [Range(1, 999, ErrorMessage = "Valor Deve Entre 1 ~ 999")]
        public int BancoId { get; set; }

        [Display(Name = "Código Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Range(1, 999, ErrorMessage = "Valor Deve Ser Entre 1 ~ 999")]
        public int CodigoBanco { get; set; }


        [Display(Name = "Nome do Banco")]
        [StringLength(30)]
        public string NomeBanco { get; set; } = String.Empty;



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


        public AgenciaDTO() { }




        public static Agencia ToEntidade(AgenciaDTO agenciaDTO)
        {
            return new Agencia
            {
                Id = agenciaDTO.Id,
                BancoId = agenciaDTO.BancoId,
                NumeroAgencia = agenciaDTO.NumeroAgencia,
                DigitoAgencia = agenciaDTO.DigitoAgencia,
                Nome = agenciaDTO.Nome
            };

        }

        public static IEnumerable<AgenciaDTO> ToDTO(IEnumerable<Agencia> Agencia)
        {
            List<AgenciaDTO> agenciaDTO = new();

            foreach (var item in Agencia)
            {
                agenciaDTO.Add(new AgenciaDTO
                {
                    Id = item.Id,
                    BancoId = item.BancoId,
                    NumeroAgencia = item.NumeroAgencia,
                    DigitoAgencia = item.DigitoAgencia,
                    Nome = item.Nome,
                    CodigoBanco = 237,
                    NomeBanco = "Bradesco"

                });
            }
            return agenciaDTO;

        }

        public static AgenciaDTO ToDTO(Agencia Agencia)
        {
            return new AgenciaDTO
            {
                Id = Agencia.Id,
                BancoId = Agencia.BancoId,
                NumeroAgencia = Agencia.NumeroAgencia,
                DigitoAgencia = Agencia.DigitoAgencia,
                Nome = Agencia.Nome
            };
        }
    }
}
