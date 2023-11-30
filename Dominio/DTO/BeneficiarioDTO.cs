using Dominio.DTO;
using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class BeneficiarioDTO
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
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Endereco { get; set; } = String.Empty;

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Numero { get; set; } = String.Empty;

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string Bairro { get; set; } = String.Empty;

        [Display(Name = "Complemento")]
        public string? Complemento { get; set; } = String.Empty;

        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = String.Empty;


        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public string CEP { get; set; } = String.Empty;





        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "UF")]
        [ForeignKey("UF")]
        public int UFId { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Município")]
        [ForeignKey("Municipio")]
        public int MunicipioId { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Tipo de Inscrição")]
        [ForeignKey("TipoInscricaoEmpresa")]
        public int TipoInscricaoEmpresaId { get; set; }





        public BeneficiarioDTO() { }




        public static Beneficiario ToEntidade(BeneficiarioDTO beneficiarioDTO)
        {
            return new Beneficiario
            {
                Id = beneficiarioDTO.Id,
                CNPJ_CPF = beneficiarioDTO.CNPJ_CPF,
                Nome = beneficiarioDTO.Nome,
                Fantasia = beneficiarioDTO.Fantasia,
                Endereco = beneficiarioDTO.Endereco,
                Numero = beneficiarioDTO.Numero,
                Bairro = beneficiarioDTO.Bairro,
                Complemento = beneficiarioDTO.Complemento,
                Cidade = beneficiarioDTO.Cidade,
                CEP = beneficiarioDTO.CEP,
                UFId = beneficiarioDTO.UFId,
                TipoInscricaoEmpresaId = beneficiarioDTO.TipoInscricaoEmpresaId,
                MunicipioId = beneficiarioDTO.MunicipioId
            };

        }

        public static IEnumerable<BeneficiarioDTO> ToDTO(IEnumerable<Beneficiario> conta)
        {
            List<BeneficiarioDTO> beneficiarioDTO = new();

            foreach (var item in conta)
            {
                beneficiarioDTO.Add(new BeneficiarioDTO
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
                    MunicipioId = item.MunicipioId,
                    TipoInscricaoEmpresaId = item.TipoInscricaoEmpresaId
                });
            }
            return beneficiarioDTO;

        }

        public static BeneficiarioDTO ToDTO(Beneficiario beneficiario)
        {
            return new BeneficiarioDTO
            {
                Id = beneficiario.Id,
                CNPJ_CPF = beneficiario.CNPJ_CPF,
                Nome = beneficiario.Nome,
                Fantasia = beneficiario.Fantasia,
                Endereco = beneficiario.Endereco,
                Numero = beneficiario.Numero,
                Bairro = beneficiario.Bairro,
                Complemento = beneficiario.Complemento,
                Cidade = beneficiario.Cidade,
                CEP = beneficiario.CEP,
                UFId = beneficiario.UFId,
                TipoInscricaoEmpresaId = beneficiario.TipoInscricaoEmpresaId,
                MunicipioId = beneficiario.MunicipioId
            };
        }
    }
}
