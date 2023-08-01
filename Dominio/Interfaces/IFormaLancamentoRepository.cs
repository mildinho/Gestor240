using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IFormaLancamentoRepository : IGenericoRepository<FormaLancamento>
    {

        Task<IEnumerable<FormaLancamento>> PesquisarPorCodigoAsync(string Codigo);
        Task<IEnumerable<FormaLancamento>> PesquisarPorDescricaoAsync(string Descricao);

    }
}
