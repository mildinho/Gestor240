using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Services
{
    public class Remessa : IRemessa
    {

        private readonly IUnitOfWork _UOW;
        private readonly ILayout _layout;
        public Remessa(IUnitOfWork unitOfWork, ILayout layout)
        {
            _UOW = unitOfWork;
            _layout = layout;
        }

        public Task Cobranca()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Pagamento(int IdBeneficiario, DateTime Inicio, DateTime Fim)
        {

            IEnumerable<Financas> ObjFinancas =  await _UOW.Financas.PesquisarPorVencimentoAsync(IdBeneficiario, Inicio, Fim);

            string nomeArquivo = await _layout.Remessa_Padrao240(ObjFinancas);

            return nomeArquivo;

        }

    }
}
