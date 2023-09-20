using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class FormaLancamentoDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Operação")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(02)]
        public string Codigo { get; set; } = String.Empty;


        [Display(Name = "Descrição do Operação")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Descricao { get; set; } = String.Empty;


        public FormaLancamentoDTO() { }




        public static FormaLancamento ToEntidade(FormaLancamentoDTO formaLancamentoDTO)
        {
            return new FormaLancamento
            {
                Id = formaLancamentoDTO.Id,
                Codigo = formaLancamentoDTO.Codigo,
                Descricao = formaLancamentoDTO.Descricao

            };

        }

        public static IEnumerable<FormaLancamentoDTO> ToDTO(IEnumerable<FormaLancamento> formaLancamento)
        {
            List<FormaLancamentoDTO> formaLancamentoDTO = new();

            foreach (var item in formaLancamento)
            {
                formaLancamentoDTO.Add(new FormaLancamentoDTO
                {
                    Id = item.Id,
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                });
            }
            return formaLancamentoDTO;

        }

        public static FormaLancamentoDTO ToDTO(FormaLancamento formaLancamento)
        {
            return new FormaLancamentoDTO
            {
                Id = formaLancamento.Id,
                Codigo = formaLancamento.Codigo,
                Descricao = formaLancamento.Descricao
            };
        }
    }
}
