﻿using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.DTO
{
    public class ContaDTO
    {

        [Key]
        [Required]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Agencia")]
        [ForeignKey("Agencia")]
        public int AgenciaId { get; set; }



        [Display(Name = "Número da Conta Corrente")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        public int NumeroConta { get; set; } = 0;

        [Display(Name = "Dígito da Conta Corrente")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(1)]
        public string DigitoConta { get; set; } = String.Empty;


        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Beneficiario")]
        [ForeignKey("Beneficiario")]
        public int BeneficiarioID { get; set; }



        [Display(Name = "Número do Convênio")]
        [Required(ErrorMessage = "Campo Obrigatório!", AllowEmptyStrings = false)]
        [StringLength(20)]
        public string NumeroConvenio { get; set; } = String.Empty;

        public int Sequencia_NSA { get; set; } = 0;


        public ContaDTO() { }




        public static Conta ToEntidade(ContaDTO contaDTO)
        {
            return new Conta
            {
                Id = contaDTO.Id,
                AgenciaId = contaDTO.AgenciaId,
                NumeroConta = contaDTO.NumeroConta,
                DigitoConta = contaDTO.DigitoConta,
                BeneficiarioID = contaDTO.BeneficiarioID,
                NumeroConvenio = contaDTO.NumeroConvenio,
                Sequencia_NSA = contaDTO.Sequencia_NSA

            };

        }

        public static IEnumerable<ContaDTO> ToDTO(IEnumerable<Conta> conta)
        {
            List<ContaDTO> contaDTO = new();

            foreach (var item in conta)
            {
                contaDTO.Add(new ContaDTO
                {
                    Id = item.Id,
                    AgenciaId = item.AgenciaId,
                    NumeroConta = item.NumeroConta,
                    DigitoConta = item.DigitoConta,
                    BeneficiarioID = item.BeneficiarioID,
                    NumeroConvenio = item.NumeroConvenio,
                    Sequencia_NSA = item.Sequencia_NSA
                });
            }
            return contaDTO;

        }

        public static ContaDTO ToDTO(Conta conta)
        {
            return new ContaDTO
            {
                Id = conta.Id,
                AgenciaId = conta.AgenciaId,
                NumeroConta = conta.NumeroConta,
                DigitoConta = conta.DigitoConta,
                BeneficiarioID = conta.BeneficiarioID,
                NumeroConvenio = conta.NumeroConvenio,
                Sequencia_NSA = conta.Sequencia_NSA
            };
        }
    }
}