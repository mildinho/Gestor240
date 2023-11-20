using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ITipoContaCorrenteRepository : IGenericoRepository<TipoContaCorrente>
    {

        Task<IEnumerable<TipoContaCorrente>> PesquisarPorDescricaoAsync(string Descricao);

    }
}
