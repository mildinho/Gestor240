using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContexto _context;
        private readonly IConfiguration _configuration;

        public IBancoRepository Banco { get; private set; }
        public ITipoOperacaoRepository TipoOperacao { get; private set; }
        public ITipoServicoRepository TipoServico { get; private set; }



        public UnitOfWork(DBContexto context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            CriaInstancia();
        }

        private void CriaInstancia()
        {
            Banco = new BancoRepository(_context, _configuration);
            TipoOperacao = new TipoOperacaoRepository(_context, _configuration);
            TipoServico = new TipoServicoRepository(_context, _configuration);

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
