using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class BancoRepository : GenericoRepository<Banco>, IBancoRepository
    {
        private readonly DBContexto _context;

        public BancoRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Banco>> PesquisarPorCodigoAsync(int Codigo)
        {
            return await _context.Banco.Where(x => x.Codigo == Codigo).ToListAsync();
        }

        public async Task<IEnumerable<Banco>> PesquisarPorNomeAsync(string Nome)
        {
            return await _context.Banco.Where(x => x.Nome.ToLower() == Nome.ToLower()).ToListAsync();
        }
    }
}
