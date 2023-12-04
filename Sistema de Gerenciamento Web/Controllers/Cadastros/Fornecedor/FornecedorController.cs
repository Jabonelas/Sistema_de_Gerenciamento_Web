using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sistema_de_Gerenciamento_Web.Class;
using Sistema_de_Gerenciamento_Web.Class.Validacoes;
using Sistema_de_Gerenciamento_Web.Context;
using Sistema_de_Gerenciamento_Web.Models.ViewModel;
using System.Net;

namespace Sistema_de_Gerenciamento_Web.Controllers.Cadastros.Fornecedor
{
    public class FornecedorController : Controller
    {
        [HttpGet]
        public IActionResult CadastrarFornecedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarFornecedor(CadastroViewModel _cadastoViewModel)
        {
            if (!IsCamposPreenchidos())
            {
                return View(_cadastoViewModel);
            }

            if (IsCpfExiste(_cadastoViewModel.registro.rg_cpf) == false || IsCnpjExiste(_cadastoViewModel.registro.rg_cnpj) == false)
            {
                InserirCadastroFornecedor(_cadastoViewModel);

                return View();
            }
            else
            {
                return View(_cadastoViewModel);
            }
        }

        private void InserirCadastroFornecedor(CadastroViewModel _cadastoViewModel)
        {
            try
            {
                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    string tipoCadastro = "Fornecedor";

                    _cadastoViewModel.registro.rg_tipo_cadastro = tipoCadastro;

                    context.tb_enderecos.Add(_cadastoViewModel.endereco);
                    context.SaveChanges();

                    _cadastoViewModel.registro.fk_endereco = _cadastoViewModel.endereco.id_endereco;

                    context.tb_registro.Add(_cadastoViewModel.registro);
                    context.SaveChanges();

                    ModelState.Clear();

                    string mensagem = "Operação realizada com sucesso!";
                    TempData["mensagemSucesso"] = mensagem;
                }
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao Inserir Dados do Fornecedor!";
            }
        }

        private bool IsCpfExiste(string _cpf)
        {
            try
            {
                if (_cpf != null)
                {
                    using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                    {
                        var isCpfExiste = context.tb_registro.Where(x => x.rg_cpf.Equals(_cpf)).Any();

                        if (isCpfExiste == true)
                        {
                            string mensagem = "CPF já Cadastrado!";
                            TempData["mensagemFalha"] = mensagem;
                        }

                        return isCpfExiste;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar CPF!";

                return true;
            }
        }

        private bool IsCamposPreenchidos()
        {
            if (!ModelState.IsValid)
            {
                foreach (var stat in ModelState.Values)
                {
                    foreach (var erro in stat.Errors)
                    {
                        ModelState.AddModelError(string.Empty, erro.ErrorMessage);
                    }
                }

                return false;
            }

            return true;
        }

        private bool IsCnpjExiste(string _cnpj)
        {
            try
            {
                if (_cnpj != null)
                {
                    using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                    {
                        var isCnpjExiste = context.tb_registro.Where(x => x.rg_cnpj.Equals(_cnpj)).Any();

                        if (isCnpjExiste == true)
                        {
                            string mensagem = "CNPJ já Cadastrado!";
                            TempData["mensagemFalha"] = mensagem;
                        }

                        return isCnpjExiste;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar CNPJ!";

                return true;
            }
        }

        [HttpGet]
        [Route("Fornecedor/GetCep/{cep}")]
        public IActionResult GetCep(string cep)
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString($"https://viacep.com.br/ws/{cep.Replace("-", "")}/json/");
                var cepModel = JsonConvert.DeserializeObject<CorreiosResponse>(json);

                EstadosBrasileiros.AdicionarEstadosBrasileiros();

                cepModel.Uf = EstadosBrasileiros.listaEstatosBrasileiros.FirstOrDefault(x => x.Contains(cepModel.Uf)).ToString();

                return Ok(cepModel);
            }
        }
    }
}