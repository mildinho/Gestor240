namespace Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IBancoRepository Banco { get; }
        ITipoOperacaoRepository TipoOperacao { get; }
        ITipoServicoRepository TipoServico { get; }



        Task<int> SaveAsync();

    }
}
