using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class EmpresaRepository : GenericoRepository<Empresa>, IEmpresaRepository
    {
        private readonly DBContexto _context;

        public EmpresaRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> PesquisarPorCNPJ_CPFAsync(double CNPJ_CPF)
        {
            return await _context.Empresa.Include(a => a.UF).Where(x => x.CNPJ_CPF == CNPJ_CPF).ToListAsync();
        }

        public async Task<IEnumerable<Empresa>> PesquisarPorNomeAsync(string Nome)
        {
            return await _context.Empresa.Include(a => a.UF).Where(x => x.Nome.ToLower() == Nome.ToLower()).ToListAsync();
        }

        public override IQueryable<Empresa> ListarTodos()
        {
            return _context.Empresa.Include( a => a.UF);
        }

    }
}
