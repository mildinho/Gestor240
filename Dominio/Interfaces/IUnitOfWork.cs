namespace Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IBancoRepository Banco { get; }
        ITipoOperacaoRepository TipoOperacao { get; }
        ITipoServicoRepository TipoServico { get; }
        IUFRepository UF { get; }
        IBeneficiarioRepository Beneficiario { get; }
        IFormaLancamentoRepository FormaLancamento { get; }
        ITipoInscricaoEmpresaRepository TipoInscricaoEmpresa { get; }
        IAgenciaRepository Agencia { get; }
        IPagadorRepository Pagador { get; }
        IFinancasRepository Financas { get; }

        Task<int> SaveAsync();

    }
}
