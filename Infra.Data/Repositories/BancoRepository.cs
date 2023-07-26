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
    public class BancoRepository : GenericoRepository<Banco>, IBancoRepository
    {
        private readonly IConfiguration _conf;
        private readonly DBContexto _context;

        public BancoRepository(DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

        public async Task<IEnumerable<Banco>> PesquisarPorCodigoAsync(string Codigo)
        {
            return await _context.Banco.Where(x => x.Codigo == Codigo).ToListAsync();
        }
    }
}
