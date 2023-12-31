﻿using Dominio.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ILayout
    {
        Task<string> Remessa_Padrao240(IEnumerable<Financas> financas, Conta conta, Beneficiario beneficiario);

        Task Retorno_Padrao240(string Arquivo);
    }
}
