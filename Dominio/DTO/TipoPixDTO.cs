using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class TipoPixDTO
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


        public TipoPixDTO() { }




        public static TipoPix ToEntidade(TipoPixDTO tipoPixDTO)
        {
            return new TipoPix
            {
                Id = tipoPixDTO.Id,
                Codigo = tipoPixDTO.Codigo,
                Descricao = tipoPixDTO.Descricao

            };

        }

        public static IEnumerable<TipoPixDTO> ToDTO(IEnumerable<TipoPix> tipoPix)
        {
            List<TipoPixDTO> tipoPixDTO = new();

            foreach (var item in tipoPix)
            {
                tipoPixDTO.Add(new TipoPixDTO
                {
                    Id = item.Id,
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                });
            }
            return tipoPixDTO;

        }

        public static TipoPixDTO ToDTO(TipoPix tipoPixDTO)
        {
            return new TipoPixDTO
            {
                Id = tipoPixDTO.Id,
                Codigo = tipoPixDTO.Codigo,
                Descricao = tipoPixDTO.Descricao

            };
        }
    }
}
