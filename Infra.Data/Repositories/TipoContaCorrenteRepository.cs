using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class TipoContaCorrenteRepository : GenericoRepository<TipoContaCorrente>, ITipoContaCorrenteRepository
    {
        private readonly DBContexto _context;

        public TipoContaCorrenteRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoContaCorrente>> PesquisarPorDescricaoAsync(string Descricao, bool PesquisaExata = false)
        {
            if (PesquisaExata)
                return await _context.TipoContaCorrente.Where(x => x.Descricao.ToLower() == Descricao.ToLower()).ToListAsync();

            return await _context.TipoContaCorrente.Where(x => x.Descricao.ToLower().Contains(Descricao.ToLower())).ToListAsync();

        }

    }
}
