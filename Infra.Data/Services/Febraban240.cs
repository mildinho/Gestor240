using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text;

namespace Infra.Data.Services
{
    public class Febraban240 : ILayout
    {
        private int SequenciaDeLote { get; set; } = 0;
        private int QtdRegistroLote { get; set; } = 0;
        private double SomatoriaLote { get; set; } = 0.0;
        private bool GerarLote { get; set; } = false;
        private int SequenciaDentroDoLote { get; set; } = 0;

        public Febraban240()
        {

        }


        public Task Retorno_Padrao240(string Arquivo)
        {
            throw new NotImplementedException();
        }


        public async Task<string> Remessa_Padrao240(IEnumerable<Financas> financas, Conta conta, Beneficiario beneficiario)
        {


            StringBuilder sb = new();
            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "arquivos\\remessa\\");
            Directory.CreateDirectory(pathFile);

            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmm") + ".rem";

            StreamWriter sw = new StreamWriter(pathFile + fileName, true, Encoding.ASCII);

            try
            {

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

                Escreve_HeaderArquivo(sw, headerArquivo);



                this.QtdRegistroLote = 0;

                var ListaServicoID = financas.GroupBy(x => x.TipoServicoID).Select(x => x.Key);
                foreach (var item in ListaServicoID)
                {
                    int tipoServicoId = item;
                    this.GerarLote = true;

                    foreach (var financa in financas.Where(x => x.TipoServicoID == tipoServicoId))
                    {

                        if (this.GerarLote)
                        {
                            this.QtdRegistroLote++;
                            this.SequenciaDeLote++;
                            this.GerarLote = false;

                            HeaderLote headerLote = new HeaderLote
                            {
                                Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
                                Lote = SequenciaDeLote.ToString("D4"),
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

                            Escreve_HeaderLote(sw, headerLote);



                        }



                        if (financa.TipoServico.Remessa_A)
                        {
                            this.SequenciaDentroDoLote++;
                            SegmentoA seguementoA = new SegmentoA
                            {
                                Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
                                Lote = SequenciaDeLote.ToString("D4"),
                                Sequencial_Registro_Lote = SequenciaDentroDoLote.ToString("D5"),
                            };

                            Escreve_SegmentoA(sw, seguementoA);
                        }




                        if (financa.TipoServico.Remessa_B)
                        {
                            this.SequenciaDentroDoLote++;
                            SegmentoB seguementoB = new SegmentoB
                            {
                                Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
                                Lote = SequenciaDeLote.ToString("D4"),
                                

                            };

                            Escreve_SegmentoB(sw, seguementoB);
                        }




                    }
                    this.QtdRegistroLote++;
                    TrailerLote trailerLote = new TrailerLote
                    {
                        Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
                        Lote = SequenciaDeLote.ToString("D4"),
                        Qtd_Lote = QtdRegistroLote.ToString("D6"),
                        Somatoria_Valor = SomatoriaLote.ToString().PadLeft(18, '0')

                    };
                    Escreve_TrailerLote(sw, trailerLote);




                }




                TrailerArquivo trailerArquivo = new TrailerArquivo
                {
                    Banco = conta.Agencia.Banco.Codigo.ToString("D3"),
                };
                Escreve_TrailerArquivo(sw, trailerArquivo);

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return String.Concat(pathFile, fileName);

        }


        private async void Escreve_HeaderArquivo(StreamWriter sw, HeaderArquivo? headerArquivo)
        {
            StringBuilder sb = new();

            if (headerArquivo != null)
            {
                sb.Append(headerArquivo.Banco);
                sb.Append(headerArquivo.Lote);
                sb.Append(headerArquivo.Registro);
                sb.Append(headerArquivo.CNAB_01);
                sb.Append(headerArquivo.TipoInscricao);
                sb.Append(headerArquivo.CNPJ_CPF);
                sb.Append(headerArquivo.Convenio);
                sb.Append(headerArquivo.Agencia);
                sb.Append(headerArquivo.AgenciaDigito);
                sb.Append(headerArquivo.Conta);
                sb.Append(headerArquivo.ContaDigito);
                sb.Append(headerArquivo.AgenciaContaDigito);
                sb.Append(headerArquivo.NomeEmpresa);
                sb.Append(headerArquivo.NomeBanco);
                sb.Append(headerArquivo.CNAB_02);
                sb.Append(headerArquivo.Remessa_Retorno);
                sb.Append(headerArquivo.DataGeracao);
                sb.Append(headerArquivo.HoraGeracao);
                sb.Append(headerArquivo.Sequencia);
                sb.Append(headerArquivo.Layout);
                sb.Append(headerArquivo.Densidade);
                sb.Append(headerArquivo.Reserva_Banco);
                sb.Append(headerArquivo.Reserva_Empresa);
                sb.Append(headerArquivo.CNAB_03);
                sb.Append('\n');


                await sw.WriteAsync(sb);
                await sw.FlushAsync();
            }


        }



        private async void Escreve_HeaderLote(StreamWriter sw, HeaderLote? headerLote)
        {
            StringBuilder sb = new();


            if (headerLote != null)
            {
                sb.Append(headerLote.Banco);
                sb.Append(headerLote.Lote);
                sb.Append(headerLote.Registro);
                sb.Append(headerLote.TipoOperacao);
                sb.Append(headerLote.TipoServico);
                sb.Append(headerLote.FormaLancamento);
                sb.Append(headerLote.VersaoLayout);
                sb.Append(headerLote.CNAB_01);
                sb.Append(headerLote.TipoInscricao);
                sb.Append(headerLote.CNPJ_CPF);
                sb.Append(headerLote.Convenio);
                sb.Append(headerLote.Agencia);
                sb.Append(headerLote.AgenciaDigito);
                sb.Append(headerLote.Conta);
                sb.Append(headerLote.ContaDigito);
                sb.Append(headerLote.AgenciaContaDigito);
                sb.Append(headerLote.NomeEmpresa);
                sb.Append(headerLote.Mensagem);
                sb.Append(headerLote.Logradouro);
                sb.Append(headerLote.Numero);
                sb.Append(headerLote.Complemento);
                sb.Append(headerLote.Cidade);
                sb.Append(headerLote.CEP);
                sb.Append(headerLote.UF);
                sb.Append(headerLote.FormaPagamento);
                sb.Append(headerLote.CNAB_02);
                sb.Append(headerLote.Ocorrencias);
                sb.Append('\n');

                await sw.WriteAsync(sb);
                await sw.FlushAsync();

            }


        }




        private async void Escreve_TrailerLote(StreamWriter sw, TrailerLote? trailerLote)
        {
            StringBuilder sb = new();

            if (trailerLote != null)
            {
                sb.Append(trailerLote.Banco);
                sb.Append(trailerLote.Lote);
                sb.Append(trailerLote.Registro);
                sb.Append(trailerLote.CNAB_01);
                sb.Append(trailerLote.Qtd_Lote);
                sb.Append(trailerLote.Somatoria_Valor);
                sb.Append(trailerLote.Qtd_Moeda);
                sb.Append(trailerLote.Numero_Aviso_Debito);
                sb.Append(trailerLote.CNAB_02);
                sb.Append(trailerLote.Ocorrencias);
                sb.Append('\n');

                await sw.WriteAsync(sb);
                await sw.FlushAsync();
            }



        }


        private async void Escreve_TrailerArquivo(StreamWriter sw, TrailerArquivo? trailerArquivo)
        {
            StringBuilder sb = new();

            if (trailerArquivo != null)
            {
                sb.Append(trailerArquivo.Banco);
                sb.Append(trailerArquivo.Lote);
                sb.Append(trailerArquivo.Registro);
                sb.Append(trailerArquivo.CNAB_01);

                sb.Append(trailerArquivo.Qtd_Lote);
                sb.Append(trailerArquivo.Qtd_Registro);
                sb.Append(trailerArquivo.Qtd_Contas);

                sb.Append(trailerArquivo.CNAB_02);
                sb.Append('\n');


                await sw.WriteAsync(sb);
                await sw.FlushAsync();
            }

        }




        private async void Escreve_SegmentoA(StreamWriter sw, SegmentoA? seguementoA)
        {
            StringBuilder sb = new();

            if (seguementoA != null)
            {
                sb.Append(seguementoA.Banco);
                sb.Append(seguementoA.Lote);
                sb.Append(seguementoA.Registro);
                sb.Append(seguementoA.CNAB_01);

                sb.Append('\n');


                await sw.WriteAsync(sb);
                await sw.FlushAsync();
            }

        }



        private async void Escreve_SegmentoB(StreamWriter sw, SegmentoB? seguementoB)
        {
            StringBuilder sb = new();

            if (seguementoB != null)
            {
                sb.Append(seguementoB.Banco);
                sb.Append(seguementoB.Lote);
                sb.Append(seguementoB.Registro);
                sb.Append('\n');


                await sw.WriteAsync(sb);
                await sw.FlushAsync();
            }

        }

    }

}

