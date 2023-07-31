using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dominio.Entidades
{

    /*
     * G028 -> Do Layout FebraBan 
     */
    [Index(nameof(Codigo), IsUnique = true)]
    [Index(nameof(Descricao))]
    public class TipoOperacao : ModelBase
    {


        [Display(Name = "Tipo de Operação")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(01)]
        public string Codigo { get; set; } = String.Empty;


        [Display(Name = "Descrição da Operacao")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Descricao { get; set; } = String.Empty;
    }
}
