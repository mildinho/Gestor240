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
    public class AgenciaRepository : GenericoRepository<Agencia>, IAgenciaRepository
    {
        private readonly DBContexto _context;

        public AgenciaRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agencia>> PesquisarPorBancoAgenciaAsync(int Banco, int Agencia)
        {
            return await _context.Agencia.Where(x => x.BancoId == Banco && x.NumeroAgencia == Agencia).ToListAsync();
        }

    }
}
