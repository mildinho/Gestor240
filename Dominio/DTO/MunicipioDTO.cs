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



        [Display(Name = "UF")]
        [StringLength(2)]
        public string SiglaUF { get; set; } = String.Empty;


        [Display(Name = "Nome da UF")]
        [StringLength(50)]
        public string NomeUF { get; set; } = String.Empty;



        [Display(Name = "Nome")]
        [StringLength(100)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Nome { get; set; } = String.Empty;



        [Display(Name = "Código Fiscal")]
        [StringLength(07)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string CodigoFiscal { get; set; } = String.Empty;


        public MunicipioDTO() { }




        public static Municipio ToEntidade(MunicipioDTO municipioDTO)
        {
            return new Municipio
            {
                Id = municipioDTO.Id,
                UFId = municipioDTO.UFId,
                Nome = municipioDTO.Nome,
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
                    Nome = item.Nome,
                    CodigoFiscal = item.CodigoFiscal,
                    SiglaUF = item.UF.Sigla,
                    NomeUF = item.UF.Descricao
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
                Nome = municipio.Nome,
                CodigoFiscal = municipio.CodigoFiscal,
                SiglaUF = municipio.UF.Sigla,
                NomeUF = municipio.UF.Descricao
            };
        }
    }
}
