using Dominio.Entidades;
using Dominio.Interfaces;
using System.Text;

namespace Infra.Data.Services
{
    public class Febraban240 : ILayout
    {

        public Febraban240()
        {

        }


        public Task Retorno_Padrao240()
        {
            throw new NotImplementedException();
        }


        public async Task<string> Remessa_Padrao240(IEnumerable<Financas> financas, Conta conta, HeaderArquivo? headerArquivo,
            HeaderLote? headerLote, TrailerLote? trailerLote, TrailerArquivo? trailerArquivo)
        {
            StringBuilder sb = new();

            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "arquivos\\remessa\\");
            Directory.CreateDirectory(pathFile);

            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmm") + ".rem";
            try
            {
                StreamWriter sw = new StreamWriter(pathFile + fileName, true, Encoding.ASCII);

                Escreve_HeaderArquivo(sw, headerArquivo);
                Escreve_HeaderLote(sw, headerLote);
                Escreve_TrailerLote(sw, trailerLote);
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
            }
            await sw.WriteAsync(sb);
            await sw.FlushAsync();


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
            }

            await sw.WriteAsync(sb);
            await sw.FlushAsync();


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
            }

            await sw.WriteAsync(sb);
            await sw.FlushAsync();
        }


    }

}

