using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ITipoOperacaoRepository : IGenericoRepository<TipoOperacao>
    {

        Task<IEnumerable<TipoOperacao>> PesquisarPorCodigoAsync(string Codigo);

    }
}
