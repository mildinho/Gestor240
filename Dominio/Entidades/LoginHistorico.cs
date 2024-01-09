using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dominio.Entidades
{
    [Index(nameof(Data))]
    public class LoginHistorico : ModelBase
    {
        public DateTime Data { get; set; }
        public string IP { get; set; }
        public string EMail { get; set; }
        public string Nome { get; set; }
        public string IdUsuario { get; set; }
    }
}
