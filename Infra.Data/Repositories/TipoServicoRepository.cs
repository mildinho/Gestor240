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
        private readonly DBContexto _context;

        public TipoServicoRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoServico>> PesquisarPorCodigoAsync(string Codigo)
        {
            return await _context.TipoServico.Where(x => x.Codigo == Codigo).ToListAsync();
        }

        public async Task<IEnumerable<TipoServico>> PesquisarPorDescricaoAsync(string Descricao)
        {
            return await _context.TipoServico.Where(x => x.Descricao.ToLower().Contains(Descricao.ToLower())).ToListAsync();
        }

    }
}
