using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class FinancasDTO 
    {
        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Beneficiario")]
        [ForeignKey("Beneficiario")]
        public int BeneficiarioID { get; set; }
      

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Pagador")]
        [ForeignKey("Pagador")]
        public int PagadorID { get; set; }
       
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "FormaLancamento")]
        [ForeignKey("FormaLancamento")]
        public int FormaLancamentoID { get; set; }
       

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "TipoServico")]
        [ForeignKey("TipoServico")]
        public int TipoServicoID { get; set; }
      



        public string Documento { get; set; } = String.Empty;
        public string Parcela { get; set; } = String.Empty;
        public DateTime Emissao { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime? Pagamento { get; set; }
        public DateTime? RegistroBanco { get; set; }
        public double ValorPrincipal { get; set; }
        public double Abatimento { get; set; }
        public double Acrescimo { get; set; }
        public double MoraDia { get; set; }

        public string Carteira { get; set; } = String.Empty;
        public string Barras { get; set; } = String.Empty;

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Banco")]
        [ForeignKey("Banco")]
        public int BancoID { get; set; }
      
        public int NumeroRemessa { get; set; }


        public string Chave_Pix { get; set; } = String.Empty;
        [Display(Name = "Tipo Pix")]
        [ForeignKey("Tipo Pix")]
        public int TipoPixID { get; set; }
        

    }
}
