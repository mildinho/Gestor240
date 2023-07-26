using Dominio.Biblioteca.Exceptions;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class GenericoRepository<Tabela> : IGenericoRepository<Tabela> where Tabela : class
    {
        private readonly DBContexto _context;
        internal DbSet<Tabela> _dbSet;


        public GenericoRepository(DBContexto context)
        {
            _context = context;
            _dbSet = context.Set<Tabela>();
        }

        public virtual async Task<Tabela> InserirAsync(Tabela tabela)
        {
            await _dbSet.AddAsync(tabela);
            return tabela;
        }



        public virtual async Task AtualizarAsync(Tabela tabela)
        {
            _context.Entry<Tabela>(tabela).State = EntityState.Modified;
            _context.Entry<Tabela>(tabela).Property("Data_Cadastro").IsModified = false;

        }



        public virtual async Task DeletarAsync(int Id)
        {
            try
            {
                var obj = await PesquisarPorIdAsync(Id);
                if (obj != null)
                {
                    _dbSet.Remove(obj);
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }



        public virtual async Task<Tabela> PesquisarPorIdAsync(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public virtual IQueryable<Tabela> ListarTodos()
        {
            return _context.Set<Tabela>();
        }

    }
}
