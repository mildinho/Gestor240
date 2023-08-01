using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class UFRepository : GenericoRepository<UF>, IUFRepository
    {
        private readonly DBContexto _context;

        public UFRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UF>> PesquisarPorDescricaoAsync(string Descricao)
        {
            return await _context.UF.Where(x => x.Descricao.ToLower().Contains(Descricao.ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<UF>> PesquisarPorSiglaAsync(string Sigla)
        {
            return await _context.UF.Where(x => x.Sigla.ToLower() == Sigla.ToLower()).ToListAsync();
        }
    }
}
