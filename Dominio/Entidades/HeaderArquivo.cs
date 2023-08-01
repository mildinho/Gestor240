using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class HeaderArquivo : ModelBase
    {
        #region Controle
        public string Banco { get; set; } = String.Empty;
        public string Lote { get; set; } = String.Empty;
        public string Registro { get; set; } = String.Empty;
        #endregion


        #region CNAB
        public string CNAB_01 { get; set; } = String.Empty;
        #endregion


        #region Empresas
        public string Tipo { get; set; } = String.Empty;
        public string Numero { get; set; } = String.Empty;
        public string Convenio { get; set; } = String.Empty;
        public string Agencia { get; set; } = String.Empty;
        public string AgenciaDigito { get; set; } = String.Empty;
        public string Conta { get; set; } = String.Empty;
        public string ContaDigito { get; set; } = String.Empty;
        public string NomeEmpresa { get; set; } = String.Empty;
        public string AgenciaContaDigito { get; set; } = String.Empty;
        #endregion


        public string NomeBanco { get; set; } = String.Empty;


        #region CNAB
        public string CNAB_02 { get; set; } = String.Empty;
        #endregion


        #region ARQUIVO
        public string Codigo { get; set; } = String.Empty;
        public string DataGeraco { get; set; } = String.Empty;
        public string HoraGeracao { get; set; } = String.Empty;
        public string Sequencia { get; set; } = String.Empty;
        public string Layout { get; set; } = String.Empty;
        public string Densidade { get; set; } = String.Empty;
        #endregion



        #region CNAB
        public string CNAB_03 { get; set; } = String.Empty;
        #endregion

    }
}
