using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IEmpresaRepository : IGenericoRepository<Empresa>
    {

        Task<IEnumerable<Empresa>> PesquisarPorCNPJ_CPFAsync(double CNPJ_CPF);
        Task<IEnumerable<Empresa>> PesquisarPorNomeAsync(string Nome);


    }
}
