using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using System.Transactions;

namespace Infra.Data.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContexto _context;
        private TransactionScope transaction;

        public void StartTransaction()
        {
            this.transaction = new TransactionScope();
        }

        public void CommitTransaction()
        {
            this.transaction.Complete();
        }

        public IBancoRepository Banco { get; private set; }
        public ITipoOperacaoRepository TipoOperacao { get; private set; }
        public ITipoServicoRepository TipoServico { get; private set; }
        public IUFRepository UF { get; private set; }
        public IEmpresaRepository Empresa { get; private set; }
        public IFormaLancamentoRepository FormaLancamento { get; private set; }
        public ITipoInscricaoEmpresaRepository TipoInscricaoEmpresa { get; private set; }


        public UnitOfWork(DBContexto context)
        {
            _context = context;

            DBManipulaDados.Cadastrar(context);

            CriaInstancia();
        }

        private void CriaInstancia()
        {
            Banco = new BancoRepository(_context);
            TipoOperacao = new TipoOperacaoRepository(_context);
            TipoServico = new TipoServicoRepository(_context);
            UF = new UFRepository(_context);
            Empresa = new EmpresaRepository(_context);
            FormaLancamento = new FormaLancamentoRepository(_context);
            TipoInscricaoEmpresa = new TipoInscricaoEmpresaRepository(_context);


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

        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }
    }
}
