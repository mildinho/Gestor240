using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dominio.Entidades
{
    public class Financas : ModelBase
    {

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Beneficiario")]
        [ForeignKey("Beneficiario")]
        public int BeneficiarioID { get; set; }
        public virtual Beneficiario? Beneficiario { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Pagador")]
        [ForeignKey("Pagador")]
        public int PagadorID { get; set; }
        public virtual Pagador? Pagador { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "FormaLancamento")]
        [ForeignKey("FormaLancamento")]
        public int FormaLancamentoID { get; set; }
        public virtual FormaLancamento? FormaLancamento { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "TipoServico")]
        [ForeignKey("TipoServico")]
        public int TipoServicoID { get; set; }
        public virtual TipoServico? TipoServico { get; set; }




        public string Documento { get; set; } = String.Empty;
        public string Parcela { get; set; } = String.Empty;
        public DateTime Emissao { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Pagamento { get; set; }
        public DateTime RegistroBanco { get; set; }
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
        public virtual Banco? Banco { get; set; }

        public int NumeroRemessa { get; set; }


        public string Chave_Pix { get; set; } = String.Empty;
        [Display(Name = "Tipo Pix")]
        [ForeignKey("Tipo Pix")]
        public int TipoPixID { get; set; }
        //public virtual TipoPix? TipoPix { get; set; }


    }
}
