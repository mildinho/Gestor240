using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public class ContaCorrenteRepository : GenericoRepository<ContaCorrente>, IContaCorrenteRepository
    {
        private readonly DBContexto _context;

        public ContaCorrenteRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContaCorrente>> PesquisarPorTipoCC_PagadorAsync(int IdTipoCC, int IdPagador)
        {
            return await _context.ContaCorrente.
                Where(x => x.TipoContaCorrenteId == IdTipoCC && 
                x.PagadorID == IdPagador
                ).
            ToListAsync();
        }

        



    }
}
