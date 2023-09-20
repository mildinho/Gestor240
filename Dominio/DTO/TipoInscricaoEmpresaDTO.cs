using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class TipoInscricaoEmpresaDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Range(1, 9, ErrorMessage = "Valor Deve Entre 1 ~ 999")]
        public int Codigo { get; set; }


        [Display(Name = "Tipo de Inscrição")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Descricao { get; set; } = String.Empty;


        public TipoInscricaoEmpresaDTO() { }




        public static TipoInscricaoEmpresa ToEntidade(TipoInscricaoEmpresaDTO tipoInscricaoEmpresaDTO)
        {
            return new TipoInscricaoEmpresa
            {
                Id = tipoInscricaoEmpresaDTO.Id,
                Codigo = tipoInscricaoEmpresaDTO.Codigo,
                Descricao = tipoInscricaoEmpresaDTO.Descricao

            };

        }

        public static IEnumerable<TipoInscricaoEmpresaDTO> ToDTO(IEnumerable<TipoInscricaoEmpresa> tipoInscricaoEmpresa)
        {
            List<TipoInscricaoEmpresaDTO> tipoInscricaoEmpresaDTO = new();

            foreach (var item in tipoInscricaoEmpresa)
            {
                tipoInscricaoEmpresaDTO.Add(new TipoInscricaoEmpresaDTO
                {
                    Id = item.Id,
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                });
            }
            return tipoInscricaoEmpresaDTO;

        }

        public static TipoInscricaoEmpresaDTO ToDTO(TipoInscricaoEmpresa tipoInscricaoEmpresa)
        {
            return new TipoInscricaoEmpresaDTO
            {
                Id = tipoInscricaoEmpresa.Id,
                Codigo = tipoInscricaoEmpresa.Codigo,
                Descricao = tipoInscricaoEmpresa.Descricao
            };
        }
    }
}
