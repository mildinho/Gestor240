using Microsoft.EntityFrameworkCore;
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
    [Index(nameof(CNPJ_CPF))]
    [Index(nameof(Nome))]
    public class Pagador : ModelBase
    {


        [Display(Name = "CNPJ / CPF")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string CNPJ_CPF { get; set; } = String.Empty;


        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Nome { get; set; } = String.Empty;


        [Display(Name = "Fantasia")]
        public string? Fantasia { get; set; } = String.Empty;


        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = String.Empty;

        [Display(Name = "Número")]
        public string Numero { get; set; } = String.Empty;

        [Display(Name = "Bairro")]
        public string Bairro { get; set; } = String.Empty;

        [Display(Name = "Complemento")]
        public string? Complemento { get; set; } = String.Empty;

        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = String.Empty;


        [Display(Name = "CEP")]
        public string CEP { get; set; } = String.Empty;





        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        [ForeignKey("UF")]
        public int UFId { get; set; }
        public virtual UF? UF { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Município")]
        [ForeignKey("Municipio")]
        public int MunicipioId { get; set; }
        public virtual Municipio? Municipio { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Tipo de Inscrição")]
        [ForeignKey("TipoInscricaoEmpresa")]
        public int TipoInscricaoEmpresaId { get; set; }
        public virtual TipoInscricaoEmpresa? TipoInscricaoEmpresa { get; set; }


    }
}
