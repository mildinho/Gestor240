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

        public async Task<string> Pagamento(int IdBeneficiario, int IDConta, DateTime Inicio, DateTime Fim)
        {
            Beneficiario beneficiario = await _UOW.Beneficiario.PesquisarPorIdAgregadoAsync(IdBeneficiario);

            if (beneficiario == null)
            {
                return "Beneficiário Não Encontrado";
            }

            Conta conta = await _UOW.Conta.PesquisarPorIdAgregadoAsync(IDConta);

            if (conta == null)
            {
                return "Conta Nao Encontrada";
            }

            if (conta.BeneficiarioID != IdBeneficiario)
            {
                return "Conta Não Pertence ao Beneficiário Informado";
            }


            IQueryable<Financas> ObjFinancas = await _UOW.Financas.TitulosPorVencimentoSemPagamentoAsync(IdBeneficiario, Inicio, Fim);

            HeaderArquivo headerArquivo = new HeaderArquivo
            {
                Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
                TipoInscricao = beneficiario.TipoInscricaoEmpresa.Codigo.ToString("D1"),
                CNPJ_CPF = beneficiario.CNPJ_CPF.PadRight(14, ' '),
                Convenio = conta.NumeroConvenio.PadRight(20, ' '),
                Agencia = conta.Agencia.NumeroAgencia.ToString("D5"),
                AgenciaDigito = conta.Agencia.DigitoAgencia.PadRight(1, ' '),
                Conta = conta.NumeroConta.ToString("D12"),
                ContaDigito = conta.DigitoConta.PadRight(1, ' '),
                NomeEmpresa = beneficiario.Nome.PadRight(30, ' '),
                NomeBanco = conta.Agencia.Banco.Nome.PadRight(30, ' '),
                Sequencia = conta.Sequencia_NSA.ToString("D6")
            };


            HeaderLote headerLote = new HeaderLote
            {
                Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
                TipoInscricao = beneficiario.TipoInscricaoEmpresa.Codigo.ToString("D1"),
                CNPJ_CPF = beneficiario.CNPJ_CPF.PadRight(14, ' '),
                Convenio = conta.NumeroConvenio.PadRight(20, ' '),
                Agencia = conta.Agencia.NumeroAgencia.ToString("D5"),
                AgenciaDigito = conta.Agencia.DigitoAgencia.PadRight(1, ' '),
                Conta = conta.NumeroConta.ToString("D12"),
                ContaDigito = conta.DigitoConta.PadRight(1, ' '),
                NomeEmpresa = beneficiario.Nome.PadRight(30, ' '),
                Logradouro = beneficiario.Endereco.PadRight(30, ' '),
                Numero = beneficiario.Numero.PadRight(5, ' '),
                Complemento = beneficiario.Complemento.PadRight(15, ' '),
                Cidade = beneficiario.Cidade.PadRight(20, ' '),
                CEP = beneficiario.CEP.PadRight(8, ' '),
                UF = beneficiario.UF.Sigla.PadRight(2, ' '),

            };
            TrailerLote trailerLote = new();

            TrailerArquivo trailerArquivo = new TrailerArquivo {
                Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
            };


            string nomeArquivo = await _layout.Remessa_Padrao240(ObjFinancas, conta, headerArquivo, headerLote, trailerLote, trailerArquivo);

            return nomeArquivo;

        }

    }
}
