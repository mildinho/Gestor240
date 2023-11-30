using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Infra.Data.Repositories
{
    public class BeneficiarioRepository : GenericoRepository<Beneficiario>, IBeneficiarioRepository
    {
        private readonly DBContexto _context;

        public BeneficiarioRepository(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<Beneficiario> PesquisarPorCNPJ_CPFAsync(string CNPJ_CPF)
        {
            CNPJ_CPF = string.Concat(CNPJ_CPF.Where(char.IsDigit));
            return await _context.Beneficiario.
                Include(a => a.UF).
                Include(b => b.TipoInscricaoEmpresa).
                Where(x => x.CNPJ_CPF == CNPJ_CPF).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Beneficiario>> PesquisarPorNomeAsync(string Nome)
        {
            return await _context.Beneficiario.
                Include(a => a.UF).
                Include(b => b.TipoInscricaoEmpresa).
                Where(x => x.Nome.ToLower() == Nome.ToLower()).ToListAsync();
        }

        public IQueryable<Beneficiario> ListarTodosAgregados()
        {
            return _context.Beneficiario.Include(a => a.UF).Include(b => b.TipoInscricaoEmpresa);
        }


        public async Task<Beneficiario> PesquisarPorIdAgregadoAsync(int Id)
        {
            return await _context.Beneficiario.
                Include(a => a.UF).
                Include(b => b.TipoInscricaoEmpresa).
                Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public int Total_Geral_Registro()
        {
            return _context.Beneficiario.Count();
        }

        public int Total_Registro_Cadastrados_Ultimos_X(int Dias = 30)
        {
            DateTime dataBase = DateTime.Now;
            dataBase = dataBase.AddDays( Dias * -1 );

            return _context.Beneficiario.Where(x => x.Data_Cadastro >= dataBase).Count();
        }


    }
}
