using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class PagadorRepository : GenericoRepository<Pagador>, IPagadorRepository
    {
        private readonly DBContexto _context;

        public PagadorRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pagador>> PesquisarPorCNPJ_CPFAsync(double CNPJ_CPF)
        {
            return await _context.Pagador.Include(a => a.UF).Where(x => x.CNPJ_CPF == CNPJ_CPF).ToListAsync();
        }

        public async Task<IEnumerable<Pagador>> PesquisarPorNomeAsync(string Nome)
        {
            return await _context.Pagador.Include(a => a.UF).Where(x => x.Nome.ToLower() == Nome.ToLower()).ToListAsync();
        }

        public override IQueryable<Pagador> ListarTodos()
        {
            return _context.Pagador.Include( a => a.UF);
        }

    }
}
