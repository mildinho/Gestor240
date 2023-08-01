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
    public class TipoInscricaoEmpresaRepository : GenericoRepository<TipoInscricaoEmpresa>, ITipoInscricaoEmpresaRepository
    {
        private readonly DBContexto _context;

        public TipoInscricaoEmpresaRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoInscricaoEmpresa>> PesquisarPorCodigoAsync(int Codigo)
        {
            return await _context.TipoInscricaoEmpresa.Where(x => x.Codigo == Codigo).ToListAsync();
        }

        public async Task<IEnumerable<TipoInscricaoEmpresa>> PesquisarPorDescricaoAsync(string Nome)
        {
            return await _context.TipoInscricaoEmpresa.Where(x => x.Descricao.ToLower() == Nome.ToLower()).ToListAsync();
        }
    }
}
