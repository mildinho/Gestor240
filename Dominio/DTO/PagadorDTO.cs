using Dominio.DTO;
using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class PagadorDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

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
       



        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Tipo de Inscrição")]
        [ForeignKey("TipoInscricaoEmpresa")]
        public int TipoInscricaoEmpresaId { get; set; }
        




        public PagadorDTO() { }




        public static Pagador ToEntidade(PagadorDTO pagadorDTO)
        {
            return new Pagador
            {
                Id = pagadorDTO.Id,
                CNPJ_CPF = pagadorDTO.CNPJ_CPF,
                Nome = pagadorDTO.Nome,
                Fantasia = pagadorDTO.Fantasia,
                Endereco = pagadorDTO.Endereco,
                Numero = pagadorDTO.Numero,
                Bairro = pagadorDTO.Bairro,
                Complemento = pagadorDTO.Complemento,
                Cidade = pagadorDTO.Cidade,
                CEP = pagadorDTO.CEP,
                UFId = pagadorDTO.UFId,
                TipoInscricaoEmpresaId = pagadorDTO.TipoInscricaoEmpresaId
            };

        }

        public static IEnumerable<PagadorDTO> ToDTO(IEnumerable<Pagador> pagador)
        {
            List<PagadorDTO> pagadorDTO = new();

            foreach (var item in pagador)
            {
                pagadorDTO.Add(new PagadorDTO
                {
                    Id = item.Id,
                    CNPJ_CPF = item.CNPJ_CPF,
                    Nome = item.Nome,
                    Fantasia = item.Fantasia,
                    Endereco = item.Endereco,
                    Numero = item.Numero,
                    Bairro = item.Bairro,
                    Complemento = item.Complemento,
                    Cidade = item.Cidade,
                    CEP = item.CEP,
                    UFId = item.UFId,
                    TipoInscricaoEmpresaId = item.TipoInscricaoEmpresaId
                });
            }
            return pagadorDTO;

        }

        public static PagadorDTO ToDTO(Pagador pagador)
        {
            return new PagadorDTO
            {
                Id = pagador.Id,
                CNPJ_CPF = pagador.CNPJ_CPF,
                Nome = pagador.Nome,
                Fantasia = pagador.Fantasia,
                Endereco = pagador.Endereco,
                Numero = pagador.Numero,
                Bairro = pagador.Bairro,
                Complemento = pagador.Complemento,
                Cidade = pagador.Cidade,
                CEP = pagador.CEP,
                UFId = pagador.UFId,
                TipoInscricaoEmpresaId = pagador.TipoInscricaoEmpresaId
            };
        }
    }
}
