using Dominio.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Biblioteca.CRUD;
using Web.Biblioteca.Filtro;
using Web.Biblioteca.msgDefault;
using Web.Biblioteca.Notification;


namespace Web.Controllers
{
    [LoginAutorizacao]
    public class CCLancamentoController : _BaseController<CCLancamentoController>
    {

        public CCLancamentoController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return await Index_Geral<PagadorDTO>("Pagador/GetAll", "Index");

        }


        [HttpGet]
        public async Task<IActionResult> Cadastrar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);
            ViewBag.TContaCorrente = await ListaTipoContaCorrente();


            ExecutaAPI.ParametrosAPI.Add(Id.ToString());

            var retornoApi = await ExecutaAPI.GetAPI("Pagador/GetbyId");
            PagadorDTO objRetorno = JsonConvert.DeserializeObject<PagadorDTO>(retornoApi.data);

            ContaCorrenteDTO contaCorrenteDTO = new ContaCorrenteDTO
            {
                CNPJ_CPF = objRetorno.CNPJ_CPF,
                Nome = objRetorno.Nome,
                Fantasia = objRetorno.Fantasia
            };


            return View("Manutencao", contaCorrenteDTO);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] ContaDTO conta, Opcoes operacao)
        {
            ViewBag.TContaCorrente = await ListaTipoContaCorrente();

            if (Opcoes.Delete == (Opcoes)operacao)
            {

                ExecutaAPI.ParametrosAPI.Add(conta.Id.ToString());

                var retornoApi = await ExecutaAPI.DeleteAPI("Conta");
                if (retornoApi.success)
                {
                    AlertNotification.Success(mensagens.MSG_S003);
                }
                else
                {
                    AlertNotification.Error(retornoApi.data);
                }


                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                ViewBag.CRUD = ConfiguraMensagem(operacao);


                if (Opcoes.Create == (Opcoes)operacao)
                {
                    var retornoApi = await ExecutaAPI.PostAPI("Conta", conta);


                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S001);
                    }
                    else
                    {
                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", conta);
                    }
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    ExecutaAPI.ParametrosAPI.Add(conta.Id.ToString());

                    var retornoApi = await ExecutaAPI.PutAPI("Conta", conta);
                    if (retornoApi.success)
                    {
                        AlertNotification.Success(mensagens.MSG_S002);
                    }
                    else
                    {
                        AlertNotification.Error(retornoApi.data);

                        return View("Manutencao", conta);
                    }


                }

                return RedirectToAction(nameof(Index));

            }


            ViewBag.CRUD = ConfiguraMensagem(operacao);

            return View();

        }



        private static CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD objCRUD = new();

            objCRUD.Titulo = "Movimentação da Conta Corrente";
            objCRUD.Descricao = "Aqui você poderá configurar seu o Movimento da Conta Corrente";
            objCRUD.SubTitulo = "Movimentação da Conta Corrente";
            objCRUD.Operacao = Opcoes.Create;

            return objCRUD;
        }



    }
}
