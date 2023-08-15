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
     * G025 -> Do Layout FebraBan 
     */
    [Index(nameof(Codigo), IsUnique = true)]
    [Index(nameof(Descricao))]
    public class TipoServico : ModelBase
    {


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



    }
}
