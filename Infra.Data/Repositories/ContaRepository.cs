using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infra.Data.Repositories
{
    public class ContaRepository : GenericoRepository<Conta>, IContaRepository
    {
        private readonly DBContexto _context;

        public ContaRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public override async Task<Conta> PesquisarPorIdAsync(int Id)
        {
            return await _context.Conta.Where(x => x.Id == Id).
                Include(x => x.Agencia).
                ThenInclude(x => x.Banco).
                FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Conta>> PesquisarPorContaAsync(int Conta)
        {
            return await _context.Conta.Where(x => x.NumeroConta == Conta ).
                ToListAsync();
        }

    }
}
