using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Dominio.DTO
{
    public class TipoServicoDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Produto / Serviço")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(02)]
        public string Codigo { get; set; } = String.Empty;


        [Display(Name = "Descrição do Produto / Serviço")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Descricao { get; set; } = String.Empty;

        public bool Remessa_A { get; set; } = false;
        public bool Remessa_B { get; set; } = false;
        public bool Remessa_C { get; set; } = false;
        public bool Remessa_D { get; set; } = false;
        public bool Remessa_H { get; set; } = false;
        public bool Remessa_I { get; set; } = false;
        public bool Remessa_J { get; set; } = false;
        public bool Remessa_J52 { get; set; } = false;
        public bool Remessa_J52Pix { get; set; } = false;
        public bool Remessa_K { get; set; } = false;
        public bool Remessa_L { get; set; } = false;
        public bool Remessa_O { get; set; } = false;
        public bool Remessa_P { get; set; } = false;
        public bool Remessa_Q { get; set; } = false;
        public bool Remessa_R { get; set; } = false;
        public bool Remessa_S { get; set; } = false;
        public bool Remessa_W { get; set; } = false;
        public bool Remessa_Y { get; set; } = false;
        public bool Remessa_Z { get; set; } = false;


        public TipoServicoDTO() { }




        public static TipoServico ToEntidade(TipoServicoDTO tipoServico)
        {
            return new TipoServico
            {
                Id = tipoServico.Id,
                Codigo = tipoServico.Codigo,
                Descricao = tipoServico.Descricao

            };

        }

        public static IEnumerable<TipoServicoDTO> ToDTO(IEnumerable<TipoServico> tipoServico)
        {
            List<TipoServicoDTO> tipoServicoDTO = new();

            foreach (var item in tipoServico)
            {
                tipoServicoDTO.Add(new TipoServicoDTO
                {
                    Id = item.Id,
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                });
            }
            return tipoServicoDTO;

        }

        public static TipoServicoDTO ToDTO(TipoServico tipoServicoDTO)
        {
            return new TipoServicoDTO
            {
                Id = tipoServicoDTO.Id,
                Codigo = tipoServicoDTO.Codigo,
                Descricao = tipoServicoDTO.Descricao

            };
        }
    }
}
