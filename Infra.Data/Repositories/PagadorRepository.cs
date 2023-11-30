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

        public async Task<Pagador> PesquisarPorCNPJ_CPFAsync(string CNPJ_CPF)
        {
            CNPJ_CPF = string.Concat(CNPJ_CPF.Where(char.IsDigit));
            return await _context.Pagador.
                 Include(a => a.UF).
                 Include(b => b.TipoInscricaoEmpresa).
                 Where(x => x.CNPJ_CPF == CNPJ_CPF).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pagador>> PesquisarPorNomeAsync(string Nome)
        {
            return await _context.Pagador.Include(a => a.UF).Where(x => x.Nome.ToLower() == Nome.ToLower()).ToListAsync();
        }

        public IQueryable<Pagador> ListarTodosAgregados()
        {
            return _context.Pagador.Include( a => a.UF);
        }

    }
}
