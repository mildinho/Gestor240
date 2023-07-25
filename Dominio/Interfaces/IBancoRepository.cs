﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IBancoRepository : IGenericoRepository<Banco>
    {

        public string ListarTodosAsync() => $"teste";

    }
}
