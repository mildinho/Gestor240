using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ITipoPixRepository : IGenericoRepository<TipoPix>
    {

        Task<IEnumerable<TipoPix>> PesquisarPorCodigoAsync(string Codigo);
        Task<IEnumerable<TipoPix>> PesquisarPorDescricaoAsync(string Descricao);

    }
}
