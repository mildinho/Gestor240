using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class SeguementoA : ModelBase
    {
        #region Controle
        public string Banco { get; set; } = String.Empty;
        public string Lote { get; set; } = String.Empty;
        public string Registro { get; set; } = "3";
        #endregion


        #region Serviços
        public string Sequencial_Registro_Lote { get; set; } = new string(' ', 5);
        public string Segmento { get; set; } = "A";
        public string Movimento_Tipo { get; set; } = new string(' ', 1);
        public string Movimento_Codigo { get; set; } = new string(' ', 2);
        #endregion


        #region Favorecido
        public string Camara { get; set; } = new string('0', 3);
        public string Banco_Favorecido { get; set; } = new string('0', 3);
        public string Agencia { get; set; } = String.Empty;
        public string AgenciaDigito { get; set; } = String.Empty;
        public string Conta { get; set; } = String.Empty;
        public string ContaDigito { get; set; } = String.Empty;
        public string AgenciaContaDigito { get; set; } = new string(' ', 1);
        public string Nome_Favorecido { get; set; } = new string(' ', 30);
        #endregion


        #region Credito
        public string SeuNumero { get; set; } = new string(' ', 20);
        public string Data_Pagamento { get; set; } = new string(' ', 8);
        public string Tipo_Moeda { get; set; } = new string(' ', 3);
        public string Quantidade_Moeda { get; set; } = new string(' ', 20);
        public string Valor_Pagamento { get; set; } = new string(' ', 20);
        public string Data_Real { get; set; } = new string(' ', 8);
        public string Valor_Real { get; set; } = new string(' ', 20);
        #endregion


        #region Informacao 2
        public string Informacao02 { get; set; } = new string(' ', 40);
        #endregion


        #region CNAB
        public string CNAB_01 { get; set; } = new string(' ', 3);
        #endregion

        public string Aviso { get; set; } = new string(' ', 1);
        public string Ocorrencias { get; set; } = new string(' ', 10);

    }
}
