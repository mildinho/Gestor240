using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class ContaRepository : GenericoRepository<Conta>, IContaRepository
    {
        private readonly DBContexto _context;

        public ContaRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<Conta> PesquisarPorIdAgregadoAsync(int Id)
        {
            return await _context.Conta.Where(x => x.Id == Id).
                Include(x => x.Agencia).
                ThenInclude(x => x.Banco).
                FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Conta>> PesquisarPorAgenciaContaAsync(int IdAgencia, int Conta)
        {
            return await _context.Conta.Where(x => x.AgenciaId == IdAgencia && x.NumeroConta == Conta ).
                ToListAsync();
        }

    }
}
