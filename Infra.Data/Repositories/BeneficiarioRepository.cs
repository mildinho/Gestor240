using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class BeneficiarioRepository : GenericoRepository<Beneficiario>, IBeneficiarioRepository
    {
        private readonly DBContexto _context;

        public BeneficiarioRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beneficiario>> PesquisarPorCNPJ_CPFAsync(double CNPJ_CPF)
        {
            return await _context.Beneficiario.Include(a => a.UF).Where(x => x.CNPJ_CPF == CNPJ_CPF).ToListAsync();
        }

        public async Task<IEnumerable<Beneficiario>> PesquisarPorNomeAsync(string Nome)
        {
            return await _context.Beneficiario.Include(a => a.UF).Where(x => x.Nome.ToLower() == Nome.ToLower()).ToListAsync();
        }

        public override IQueryable<Beneficiario> ListarTodos()
        {
            return _context.Beneficiario.Include( a => a.UF);
        }

    }
}
