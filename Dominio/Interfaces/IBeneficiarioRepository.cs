using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IBeneficiarioRepository : IGenericoRepository<Beneficiario>
    {

        Task<IEnumerable<Beneficiario>> PesquisarPorCNPJ_CPFAsync(string CNPJ_CPF);
        Task<IEnumerable<Beneficiario>> PesquisarPorNomeAsync(string Nome);
        Task<Beneficiario> PesquisarPorIdAgregadoAsync(int Id);
        IQueryable<Beneficiario> ListarTodosAgregados();




    }
}
