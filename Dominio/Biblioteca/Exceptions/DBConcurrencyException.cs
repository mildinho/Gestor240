using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Biblioteca.Exceptions
{
    public class DBConcurrencyException : ApplicationException
    {
        public DBConcurrencyException(string msg) : base(msg)
        {

        }
    }
}
