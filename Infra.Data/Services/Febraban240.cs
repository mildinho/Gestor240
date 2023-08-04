using Dominio.Entidades;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Services
{
    public class Febraban240 : ILayout
    {

        public Febraban240()
        {

        }




        public async Task<string> Remessa_Padrao240(IEnumerable<Financas> financas)
        {

            financas.OrderBy(x => x.FormaLancamento).ThenBy(x => x.TipoServico);

            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "arquivos\\remessa\\");
            Directory.CreateDirectory(pathFile);

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".rem";
            try
            {
                StreamWriter sw = new StreamWriter(pathFile + fileName, true, Encoding.ASCII);

                await sw.WriteAsync("fer");
                
                
                
                await sw.FlushAsync();



                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return String.Concat(pathFile,fileName);

        }

        public Task Retorno_Padrao240()
        {
            throw new NotImplementedException();
        }
    }
}
