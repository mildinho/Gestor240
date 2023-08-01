﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IBancoRepository : IGenericoRepository<Banco>
    {

        Task<IEnumerable<Banco>> PesquisarPorCodigoAsync(int Codigo);
        Task<IEnumerable<Banco>> PesquisarPorNomeAsync(string Descricao);


    }
}
