using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IAgenciaRepository : IGenericoRepository<Agencia>
    {

        Task<IEnumerable<Agencia>> PesquisarPorBancoAgenciaAsync(int IdBanco, int Agencia);


    }
}
