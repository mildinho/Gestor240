namespace Dominio.Interfaces
{
    public interface IUnitOfWork 
    {

        IBancoRepository Banco { get; }
        IAgenciaRepository Agencia { get; }
        IContaRepository Conta { get; }
        IUFRepository UF { get; }
        IMunicipioRepository Municipio { get; }

        ITipoOperacaoRepository TipoOperacao { get; }
        ITipoServicoRepository TipoServico { get; }
        ITipoPixRepository TipoPix { get; }
        ITipoContaCorrenteRepository TipoContaCorrente { get; }

        IFormaLancamentoRepository FormaLancamento { get; }
        ITipoInscricaoEmpresaRepository TipoInscricaoEmpresa { get; }

        IBeneficiarioRepository Beneficiario { get; }
        IPagadorRepository Pagador { get; }
        IFinancasRepository Financas { get; }

        ILoginRepository Login { get; }

        Task<int> SaveAsync();

    }
}
