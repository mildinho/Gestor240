using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class MunicipioDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        [ForeignKey("UF")]
        public int UFId { get; set; }
      

        [Display(Name = "Descrição")]
        [StringLength(100)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; } = String.Empty;



        [Display(Name = "Código Fiscal")]
        [StringLength(07)]
        public string CodigoFiscal { get; set; } = String.Empty;


        public MunicipioDTO() { }




        public static Municipio ToEntidade(MunicipioDTO municipioDTO)
        {
            return new Municipio
            {
                Id = municipioDTO.Id,
                UFId = municipioDTO.UFId,
                Descricao = municipioDTO.Descricao,
                CodigoFiscal = municipioDTO.CodigoFiscal

            };

        }

        public static IEnumerable<MunicipioDTO> ToDTO(IEnumerable<Municipio> municipios)
        {
            List<MunicipioDTO> municipioDTO = new();

            foreach (var item in municipios)
            {
                municipioDTO.Add(new MunicipioDTO
                {
                    Id = item.Id,
                    UFId = item.UFId,
                    Descricao = item.Descricao,
                    CodigoFiscal = item.CodigoFiscal
                });
            }
            return municipioDTO;

        }

        public static MunicipioDTO ToDTO(Municipio municipio)
        {
            return new MunicipioDTO
            {
                Id = municipio.Id,
                UFId = municipio.UFId,
                Descricao = municipio.Descricao,
                CodigoFiscal = municipio.CodigoFiscal
            };
        }
    }
}
