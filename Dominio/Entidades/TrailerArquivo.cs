using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class TrailerArquivo : ModelBase
    {

        #region Controle
        public string Banco { get; set; } = String.Empty;
        public string Lote { get; set; } = String.Empty;
        public string Registro { get; set; } = String.Empty;
        #endregion


        #region CNAB
        public string CNAB_01 { get; set; } = String.Empty;
        #endregion

        #region TOTAIS
        public string Qtd_Lote { get; set; } = String.Empty;
        public string Qtd_Registro { get; set; } = String.Empty;
        public string Qtd_Contas { get; set; } = String.Empty;
        #endregion


        #region CNAB
        public string CNAB_02 { get; set; } = String.Empty;
        #endregion

    }
}
