using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{

    [Index(nameof(TipoContaCorrenteId), nameof(Data) )]

    public class ContaCorrente : ModelBase
    {

        
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Tipo Conta Corrente")]
        [ForeignKey("TipoContaCorrente")]
        public int TipoContaCorrenteId { get; set; }
        public virtual TipoContaCorrente? TipoContaCorrente { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Pagador")]
        [ForeignKey("Pagador")]
        public int PagadorID { get; set; }
        public virtual Pagador? Pagador { get; set; }



        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        [Display(Name = "D/C")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string D_C { get; set; } = String.Empty;


        [Display(Name = "Historico")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(300)]
        public string Historico { get; set; } = String.Empty;

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double Valor { get; set; } = 0;


    }
}
