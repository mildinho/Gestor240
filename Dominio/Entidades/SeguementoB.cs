using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class SeguementoB : ModelBase
    {
        #region Controle
        public string Banco { get; set; } = String.Empty;
        public string Lote { get; set; } = String.Empty;
        public string Registro { get; set; } = "3";
        #endregion


    }
}
