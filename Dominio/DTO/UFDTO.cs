using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Dominio.DTO
{
    public class UFDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(2)]
        public string Sigla { get; set; } = String.Empty;

        [Display(Name = "Descrição")]
        [StringLength(50)]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Descricao { get; set; } = String.Empty;



        [Display(Name = "Código Fiscal")]
        [StringLength(02)]
        public string CodigoFiscal { get; set; } = String.Empty;


        public UFDTO() { }




        public static UF ToEntidade(UFDTO ufDTO)
        {
            return new UF
            {
                Id = ufDTO.Id,
                Sigla = ufDTO.Sigla,
                Descricao = ufDTO.Descricao,
                CodigoFiscal = ufDTO.CodigoFiscal

            };

        }

        public static IEnumerable<UFDTO> ToDTO(IEnumerable<UF> uf)
        {
            List<UFDTO> ufDTO = new();

            foreach (var item in uf)
            {
                ufDTO.Add(new UFDTO
                {
                    Id = item.Id,
                    Sigla = item.Sigla,
                    Descricao = item.Descricao,
                    CodigoFiscal = item.CodigoFiscal
                });
            }
            return ufDTO;

        }

        public static UFDTO ToDTO(UF ufDTO)
        {
            return new UFDTO
            {
                Id = ufDTO.Id,
                Sigla = ufDTO.Sigla,
                Descricao = ufDTO.Descricao,
                CodigoFiscal = ufDTO.CodigoFiscal

            };
        }
    }
}
