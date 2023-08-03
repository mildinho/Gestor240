using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class HeaderArquivo : ModelBase
    {
        #region Controle
        
        [MaxLength(3)]
        public string Banco { get; set; } = String.Empty;

        [MaxLength(4)]
        public string Lote { get; set; } = "0000";

        [MaxLength(1)]
        public string Registro { get; set; } = "0";
        #endregion


        #region CNAB
        [MaxLength(9)]
        public string CNAB_01 { get; set; } = new string(' ', 9);
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


        [MaxLength(30)]
        public string NomeBanco { get; set; } = String.Empty;


        #region CNAB
        [MaxLength(10)]
        public string CNAB_02 { get; set; } = new string(' ', 10);
        #endregion


        #region ARQUIVO
        [MaxLength(1)]
        public string Remessa_Retorno { get; set; } = String.Empty;
        
        [MaxLength(8)]
        public string DataGeraco { get; set; } = String.Empty;

        [MaxLength(6)]
        public string HoraGeracao { get; set; } = String.Empty;

        [MaxLength(6)]
        public string Sequencia { get; set; } = String.Empty;

        [MaxLength(3)]
        public string Layout { get; set; } = String.Empty;

        [MaxLength(5)]
        public string Densidade { get; set; } = String.Empty;
        public string Reserva_Banco { get; set; } = new string(' ', 20);
        public string Reserva_Empresa { get; set; } = new string(' ', 20);
        #endregion



        #region CNAB
        [MaxLength(29)]
        public string CNAB_03 { get; set; } = new string(' ', 29);
        #endregion

    }
}
