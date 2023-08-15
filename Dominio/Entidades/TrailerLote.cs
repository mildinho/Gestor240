using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class TrailerLote : ModelBase
    {

        #region Controle
        public string Banco { get; set; } = String.Empty;
        public string Lote { get; set; } = String.Empty;
        public string Registro { get; set; } = "5";
        #endregion


        #region CNAB
        public string CNAB_01 { get; set; } = new string(' ', 9);
        #endregion


        #region TOTAIS
        public string Qtd_Lote { get; set; } = new string('0', 6);
        public string Somatoria_Valor { get; set; } = new string('0', 18);
        public string Qtd_Moeda { get; set; } = new string('0', 18);
        #endregion

        public string Numero_Aviso_Debito { get; set; } = new string('0', 6);

        #region CNAB
        public string CNAB_02 { get; set; } = new string(' ', 165);
        #endregion

        public string Ocorrencias { get; set; } = new string(' ', 10);


    }
}
