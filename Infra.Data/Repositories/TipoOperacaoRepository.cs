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
    public class TipoOperacaoRepository : GenericoRepository<TipoOperacao>, ITipoOperacaoRepository
    {
        private readonly DBContexto _context;

        public TipoOperacaoRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoOperacao>> PesquisarPorCodigoAsync(string Codigo)
        {
            return await _context.TipoOperacao.Where(x => x.Codigo == Codigo).ToListAsync();
        }

        public async Task<IEnumerable<TipoOperacao>> PesquisarPorDescricaoAsync(string Descricao)
        {
            return await _context.TipoOperacao.Where(x => x.Descricao.ToLower().Contains(Descricao.ToLower())).ToListAsync();
        }

    }
}
