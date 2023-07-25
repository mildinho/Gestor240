using Dominio.Biblioteca.Enums;
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
    public class Empresa : ModelBase
    {

        [Display(Name = "CNPJ / CPF")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public double CNPJ_CPF { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Nome { get; set; } = String.Empty;


        [Display(Name = "Fantasia")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Fantasia { get; set; } = String.Empty;


        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = String.Empty;

        [Display(Name = "Número")]
        public string Numero { get; set; } = String.Empty;

        [Display(Name = "Bairro")]
        public string Bairro { get; set; } = String.Empty;

        [Display(Name = "Complemento")]
        public string Complemento { get; set; } = String.Empty;

        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = String.Empty;


        [Display(Name = "CEP")]
        public string CEP { get; set; } = String.Empty;





        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        [ForeignKey("UF")]
        public int UFId { get; set; }
        public virtual UF UF { get; set; }

        public TipoInscricaoEmpresa TipoInscricao { get; set; }

    }
}
