using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Biblioteca.Exceptions
{
    public class IntegrityException : ApplicationException

    {
        public IntegrityException(string message) : base(message)
        {
        }
    }
}
