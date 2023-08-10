using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IContaRepository : IGenericoRepository<Conta>
    {

        Task<IEnumerable<Conta>> PesquisarPorContaAsync(int Conta);
        Task<Conta> PesquisarPorIdAgregadoAsync(int Id);

    }
}
