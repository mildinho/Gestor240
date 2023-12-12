using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ILoginHistoricoRepository : IGenericoRepository<LoginHistorico>
    {
        Task<IEnumerable<LoginHistorico>> PesquisarPorDataAsync(DateTime Inicio, DateTime Fim);

    }
}
