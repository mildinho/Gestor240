using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ITipoInscricaoEmpresaRepository : IGenericoRepository<TipoInscricaoEmpresa>
    {

        Task<IEnumerable<TipoInscricaoEmpresa>> PesquisarPorCodigoAsync(int Codigo);
        Task<IEnumerable<TipoInscricaoEmpresa>> PesquisarPorDescricaoAsync(string Descricao);


    }
}
