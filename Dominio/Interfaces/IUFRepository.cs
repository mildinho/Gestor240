using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUFRepository : IGenericoRepository<UF>
    {

        Task<IEnumerable<UF>> PesquisarPorSiglaAsync(string Sigla);

        Task<IEnumerable<UF>> PesquisarPorDescricaoAsync(string Descricao);

    }
}
