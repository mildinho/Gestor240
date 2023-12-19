using Dominio.DTO;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Models
{
    public class CCViewModel
    {
        public PagadorVM Pagador { get; set; }
        public List<ContaCorrenteDTO> ListaCCDTO { get; set; }
        public ContaCorrenteDTO ContaCorrenteDTO { get; set; }
        
    }

    public class PagadorVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "CNPJ / CPF")]
        public string CNPJ_CPF { get; set; } = String.Empty;


        [Display(Name = "Nome")]
        public string Nome { get; set; } = String.Empty;

        [Display(Name = "Fantasia")]
        public string Fantasia { get; set; } = String.Empty;
    }



}
