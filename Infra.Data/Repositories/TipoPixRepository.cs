using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class TipoPixRepository : GenericoRepository<TipoPix>, ITipoPixRepository
    {
        private readonly DBContexto _context;

        public TipoPixRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoPix>> PesquisarPorCodigoAsync(string Codigo)
        {
            return await _context.TipoPix.Where(x => x.Codigo == Codigo).ToListAsync();
        }

        public async Task<IEnumerable<TipoPix>> PesquisarPorDescricaoAsync(string Descricao)
        {
            return await _context.TipoPix.Where(x => x.Descricao.ToLower().Contains(Descricao.ToLower())).ToListAsync();
        }

    }
}
