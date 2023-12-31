﻿using System.ComponentModel.DataAnnotations;

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
        public string CNPJ_CPF { get; set; } = String.Empty;

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
        public string AgenciaContaDigito { get; set; } = new string(' ', 1);

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
        public string Remessa_Retorno { get; set; } = "1";
        
        [MaxLength(8)]
        public string DataGeracao { get; set; } = DateTime.Now.ToString("ddMMyyyy");

        [MaxLength(6)]
        public string HoraGeracao { get; set; } = DateTime.Now.ToString("HHmmss");

        [MaxLength(6)]
        public string Sequencia { get; set; } = String.Empty;

        [MaxLength(3)]
        public string Layout { get; set; } = "103";

        [MaxLength(5)]
        public string Densidade { get; set; } = "01600";
        public string Reserva_Banco { get; set; } = new string(' ', 20);
        public string Reserva_Empresa { get; set; } = new string(' ', 20);
        #endregion



        #region CNAB
        [MaxLength(29)]
        public string CNAB_03 { get; set; } = new string(' ', 29);
        #endregion

    }
}
