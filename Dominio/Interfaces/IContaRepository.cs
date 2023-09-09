using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IContaRepository : IGenericoRepository<Conta>
    {

        Task<IEnumerable<Conta>> PesquisarPorAgenciaContaAsync(int IdAgencia, int Conta);
        Task<Conta> PesquisarPorIdAgregadoAsync(int Id);

    }
}
