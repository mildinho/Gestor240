using Dominio.Interfaces;
using Infra.Data.Contexto;


namespace Infra.Data.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContexto _context;

        public IBancoRepository Banco { get; private set; }
        public ITipoOperacaoRepository TipoOperacao { get; private set; }
        public ITipoServicoRepository TipoServico { get; private set; }
        public IUFRepository UF { get; private set; }
        public IMunicipioRepository Municipio { get; private set; }

        public IBeneficiarioRepository Beneficiario { get; private set; }
        public IFormaLancamentoRepository FormaLancamento { get; private set; }
        public ITipoInscricaoEmpresaRepository TipoInscricaoEmpresa { get; private set; }
        public IAgenciaRepository Agencia { get; private set; }

        public IContaRepository Conta { get; private set; }
        public IPagadorRepository Pagador { get; private set; }
        public IFinancasRepository Financas { get; private set; }
        public ITipoPixRepository TipoPix { get; private set; }
        public ITipoContaCorrenteRepository TipoContaCorrente { get; private set; }

        public ILoginRepository Login { get; private set; }
        public ILoginHistoricoRepository LoginHistorico { get; private set; }


        public UnitOfWork(DBContexto context)
        {
            _context = context;


            DBManipulaDados.Cadastrar(context);


            Banco = new BancoRepository(_context);
            TipoOperacao = new TipoOperacaoRepository(_context);
            TipoServico = new TipoServicoRepository(_context);
            UF = new UFRepository(_context);
            Municipio = new MunicipioRepository(_context);
            Beneficiario = new BeneficiarioRepository(_context);
            FormaLancamento = new FormaLancamentoRepository(_context);
            TipoInscricaoEmpresa = new TipoInscricaoEmpresaRepository(_context);
            Agencia = new AgenciaRepository(_context);
            Conta = new ContaRepository(_context);
            Pagador = new PagadorRepository(_context);
            Financas = new FinancasRepository(_context);
            TipoPix = new TipoPixRepository(_context);
            TipoContaCorrente = new TipoContaCorrenteRepository(_context);
            Login = new LoginRepository(_context);
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

       
    }
}
