using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Dominio.DTO
{
    public class BancoDTO
    {

        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Range(1, 999, ErrorMessage = "Valor Deve Ser Entre 1 ~ 999")]
        public int Codigo { get; set; }


        [Display(Name = "Nome do Banco")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Nome { get; set; } = String.Empty;

        [Display(Name = "Código ISPB ")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(10)]
        public string ISPB { get; set; } = String.Empty;


        public BancoDTO() { }




        public static Banco ToEntidade(BancoDTO bancoDTO)
        {
            return new Banco
            {
                Id = bancoDTO.Id,
                Codigo = bancoDTO.Codigo,
                Nome = bancoDTO.Nome,
                ISPB = bancoDTO.ISPB

            };

        }

        public static IEnumerable<BancoDTO> ToDTO(IEnumerable<Banco> banco)
        {
            List<BancoDTO> bancoDTO = new();

            foreach (var item in banco)
            {
                bancoDTO.Add(new BancoDTO
                {
                    Id = item.Id,
                    Codigo = item.Codigo,
                    Nome = item.Nome,
                    ISPB = item.ISPB
                });
            }
            return bancoDTO;

        }

        public static BancoDTO ToDTO(Banco banco)
        {
            return new BancoDTO
            {
                Id = banco.Id,
                Codigo = banco.Codigo,
                Nome = banco.Nome,
                ISPB = banco.ISPB
            };
        }
    }
}
