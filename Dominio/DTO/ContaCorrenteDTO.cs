using Dominio.Entidades;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class ContaCorrenteDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Tipo Conta Corrente")]
        [ForeignKey("TipoContaCorrente")]
        public int TipoContaCorrenteId { get; set; }
     

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Pagador")]
        [ForeignKey("Pagador")]
        public int PagadorID { get; set; }


        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; } = DateTime.Today;

        [Display(Name = "D/C")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string D_C { get; set; } = "D";


        [Display(Name = "Historico")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(300)]
        public string Historico { get; set; } = String.Empty;

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Valor { get; set; } = 0.00;

        public ContaCorrenteDTO() { }




        public static ContaCorrente ToEntidade(ContaCorrenteDTO ccDTO)
        {
            return new ContaCorrente
            {
                Id = ccDTO.Id,
                Data = ccDTO.Data,
                D_C = ccDTO.D_C,
                Historico = ccDTO.Historico,
                Valor = ccDTO.Valor,
                TipoContaCorrenteId = ccDTO.TipoContaCorrenteId,
                PagadorID = ccDTO.PagadorID

            };
        }

        public static IEnumerable<ContaCorrenteDTO> ToDTO(IEnumerable<ContaCorrente> cc)
        {
            List<ContaCorrenteDTO> ccDTO = new();

            foreach (var item in cc)
            {
                ccDTO.Add(new ContaCorrenteDTO
                {
                    Id = item.Id,
                    Data = item.Data,
                    D_C = item.D_C,
                    Historico = item.Historico,
                    Valor = item.Valor,
                    TipoContaCorrenteId = item.TipoContaCorrenteId,
                    PagadorID = item.PagadorID
                });
            }
            return ccDTO;

        }

        public static ContaCorrenteDTO ToDTO(ContaCorrente cc)
        {
            return new ContaCorrenteDTO
            {
                Id = cc.Id,
                Data = cc.Data,
                D_C = cc.D_C,
                Historico = cc.Historico,
                Valor = cc.Valor,
                TipoContaCorrenteId = cc.TipoContaCorrenteId,
                PagadorID = cc.PagadorID

            };
        }
    }
}
