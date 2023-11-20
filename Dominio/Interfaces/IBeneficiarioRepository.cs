using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IBeneficiarioRepository : IGenericoRepository<Beneficiario>
    {

        Task<Beneficiario> PesquisarPorCNPJ_CPFAsync(string CNPJ_CPF);
        Task<IEnumerable<Beneficiario>> PesquisarPorNomeAsync(string Nome);
        Task<Beneficiario> PesquisarPorIdAgregadoAsync(int Id);
        IQueryable<Beneficiario> ListarTodosAgregados();




    }
}
