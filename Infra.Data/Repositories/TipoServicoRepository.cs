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
    public class TipoServicoRepository : GenericoRepository<TipoServico>, ITipoServicoRepository
    {
        private readonly IConfiguration _conf;
        private readonly DBContexto _context;

        public TipoServicoRepository(DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

        public async Task<IEnumerable<TipoServico>> PesquisarPorCodigoAsync(string Codigo)
        {
            return await _context.TipoServico.Where(x => x.Codigo == Codigo).ToListAsync();
        }
    }
}
