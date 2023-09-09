using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IBancoRepository : IGenericoRepository<Banco>
    {

        Task<IEnumerable<Banco>> PesquisarPorCodigoAsync(int Codigo);
        Task<IEnumerable<Banco>> PesquisarPorNomeAsync(string Nome);


    }
}
