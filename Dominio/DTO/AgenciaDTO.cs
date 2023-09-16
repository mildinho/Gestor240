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
        public int BancoId { get; set; }
        


        [Display(Name = "Número da Agência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(5)]
        public int NumeroAgencia { get; set; } = 0;

        [Display(Name = "Dígito da Agência")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string DigitoAgencia { get; set; } = String.Empty;


        [Display(Name = "Nome")]
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
                    Nome = item.Nome
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
