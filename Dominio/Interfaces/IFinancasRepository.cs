using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IFinancasRepository : IGenericoRepository<Financas>
    {

        Task<IEnumerable<Financas>> PesquisarPorVencimentoAsync(int Beneficiario, DateTime Inicio, DateTime Fim);


    }
}
