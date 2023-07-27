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
            _context.Entry<Tabela>(tabela).Property("Id").CurrentValue = 0;
            _context.Entry<Tabela>(tabela).Property("Data_Alteracao").CurrentValue = DateTime.Now;
            _context.Entry<Tabela>(tabela).Property("Data_Cadastro").CurrentValue = DateTime.Now;

            await _dbSet.AddAsync(tabela);
            return tabela;
        }



        public virtual async Task<Tabela> AtualizarAsync(Tabela tabela)
        {
            var _Id = _context.Entry<Tabela>(tabela).Property("Id").CurrentValue;
            
            try
            {
                
                var obj = await PesquisarPorIdAsync((int) _Id);
                if (obj != null)
                {
                    _context.Entry<Tabela>(tabela).Property("Data_Cadastro").CurrentValue =
                    _context.Entry<Tabela>(obj).Property("Data_Cadastro").CurrentValue;
                    _dbSet.Update(tabela);
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


            return tabela;
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
