using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class TipoOperacaoDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Operação")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(01)]
        public string Codigo { get; set; } = String.Empty;


        [Display(Name = "Descrição do Operação")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Descricao { get; set; } = String.Empty;


        public TipoOperacaoDTO() { }




        public static TipoOperacao ToEntidade(TipoOperacaoDTO tipoOperacaoDTO)
        {
            return new TipoOperacao
            {
                Id = tipoOperacaoDTO.Id,
                Codigo = tipoOperacaoDTO.Codigo,
                Descricao = tipoOperacaoDTO.Descricao

            };

        }

        public static IEnumerable<TipoOperacaoDTO> ToDTO(IEnumerable<TipoOperacao> tipoOperacao)
        {
            List<TipoOperacaoDTO> tipoOperacaoDTO = new();

            foreach (var item in tipoOperacao)
            {
                tipoOperacaoDTO.Add(new TipoOperacaoDTO
                {
                    Id = item.Id,
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                });
            }
            return tipoOperacaoDTO;

        }

        public static TipoOperacaoDTO ToDTO(TipoOperacao tipoOperacao)
        {
            return new TipoOperacaoDTO
            {
                Id = tipoOperacao.Id,
                Codigo = tipoOperacao.Codigo,
                Descricao = tipoOperacao.Descricao
            };
        }
    }
}
