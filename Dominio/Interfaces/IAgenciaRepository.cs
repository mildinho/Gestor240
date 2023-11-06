using Dominio.Entidades;
using System.Reflection.Emit;

namespace Dominio.Interfaces
{
    public interface IAgenciaRepository : IGenericoRepository<Agencia>
    {

        Task<IEnumerable<Agencia>> PesquisarPorBancoAgenciaAsync(int IdBanco, int Agencia);
        Task<IEnumerable<Agencia>> PesquisarPorBancoAgenciaAgregadoAsync(int IdBanco, int Agencia);
        Task<Agencia> PesquisarPorIdAgregadoAsync(int Id);
        Task<IQueryable<Agencia>> ListarTodosAgregados();

    }
}
