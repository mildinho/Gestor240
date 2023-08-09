using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IContaRepository : IGenericoRepository<Conta>
    {

        Task<IEnumerable<Conta>> PesquisarPorContaAsync(int Conta);


    }
}
