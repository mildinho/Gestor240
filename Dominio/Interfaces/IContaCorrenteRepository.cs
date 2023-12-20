using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IContaCorrenteRepository : IGenericoRepository<ContaCorrente>
    {
        Task<IEnumerable<ContaCorrente>> PesquisarPorTipoCC_PagadorAsync(int IdTipoCC, int IdPagador);
    }
}
