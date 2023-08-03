using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class HeaderLote : ModelBase
    {

        #region Controle

        [MaxLength(3)]
        public string Banco { get; set; } = String.Empty;

        [MaxLength(4)]
        public string Lote { get; set; } = "0001";

        [MaxLength(1)]
        public string Registro { get; set; } = "1";

        [MaxLength(1)]
        public string TipoOperacao { get; set; } = "C";
        #endregion


        #region Serviços

        [MaxLength(2)]
        public string TipoServico { get; set; } = new string(' ', 2);

        [MaxLength(2)]
        public string FormaLancamento { get; set; } = new string(' ', 2);

        [MaxLength(3)]
        public string VersaoLayout { get; set; } = new string(' ', 3);

        #endregion


        #region CNAB
        [MaxLength(1)]
        public string CNAB_01 { get; set; } = new string(' ', 1);
        #endregion


        #region Empresas
        [MaxLength(1)]
        public string TipoInscricao { get; set; } = String.Empty;

        [MaxLength(14)]
        public string CNPJ { get; set; } = String.Empty;

        [MaxLength(20)]
        public string Convenio { get; set; } = String.Empty;

        [MaxLength(05)]
        public string Agencia { get; set; } = String.Empty;

        [MaxLength(1)]
        public string AgenciaDigito { get; set; } = String.Empty;

        [MaxLength(12)]
        public string Conta { get; set; } = String.Empty;

        [MaxLength(1)]
        public string ContaDigito { get; set; } = String.Empty;

        [MaxLength(1)]
        public string AgenciaContaDigito { get; set; } = String.Empty;

        [MaxLength(30)]
        public string NomeEmpresa { get; set; } = String.Empty;
        #endregion


        [MaxLength(40)]
        public string Mensagem { get; set; } = String.Empty;


        #region Endereco_Empresa
        [MaxLength(30)]
        public string Logradouro { get; set; } = String.Empty;

        [MaxLength(05)]
        public string Numero { get; set; } = String.Empty;

        [MaxLength(15)]
        public string Complemento { get; set; } = String.Empty;

        [MaxLength(20)]
        public string Cidade { get; set; } = String.Empty;

        [MaxLength(8)]
        public string CEP { get; set; } = String.Empty;

        [MaxLength(02)]
        public string UF { get; set; } = String.Empty;

        #endregion

        [MaxLength(02)]
        public string FormaPagamento { get; set; } = String.Empty;


        #region CNAB
        [MaxLength(6)]
        public string CNAB_02 { get; set; } = new string(' ', 6);
        #endregion


        [MaxLength(10)]
        public string Ocorrencias { get; set; } = String.Empty;

    }
}
