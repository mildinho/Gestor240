using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class SegmentoB : ModelBase
    {
        #region Controle
        public string Banco { get; set; } = new string('0', 3);
        public string Lote { get; set; } = new string('0', 4);
        public string Registro { get; set; } = "3";
        #endregion

        #region Servico
        public string Sequencial_Registro_Lote { get; set; } = new string('0', 5);
        public string Segmento { get; set; } = "B";
        #endregion


        public string FormaIniciacao { get; set; } = new string(' ', 3);



        #region Inscricao
        public string TipoInscricao { get; set; } = new string('0', 1);
        public string Numero { get; set; } = new string('0', 14);
        #endregion


        #region Dados Complementares
        public string Informacao_10 { get; set; } = new string(' ', 35);
        public string Informacao_11 { get; set; } = new string(' ', 60);
        public string Informacao_12 { get; set; } = new string(' ', 99);
        #endregion

        public string Codigo_SIAPE { get; set; } = new string('0', 6);
        public string Codigo_ISPB { get; set; } = new string('0', 8);

    }
}
