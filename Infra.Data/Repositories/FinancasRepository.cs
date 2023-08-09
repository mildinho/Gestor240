using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class FinancasRepository : GenericoRepository<Financas>, IFinancasRepository
    {
        private readonly DBContexto _context;

        public FinancasRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<Financas>> TitulosPorVencimentoSemPagamentoAsync(int Beneficiario, DateTime Inicio, DateTime Fim)
        {
            return  _context.Financas.
                Where(x => x.BeneficiarioID == Beneficiario &&
                x.Vencimento >= Inicio && x.Vencimento <= Fim && 
                x.Pagamento == null).
                Include(x => x.Beneficiario).
                Include(x => x.Pagador).
                Include(x => x.FormaLancamento).
                Include(x => x.TipoServico).
                Include(x => x.Banco);
                
        }

    }
}
