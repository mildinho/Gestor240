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

        public async Task<IEnumerable<Financas>> PesquisarPorBeneficiarioAsync(int Beneficiario)
        {
            return await _context.Financas.
                Where(x => x.BeneficiarioID == Beneficiario)
                .ToListAsync();
        }
    }
}
