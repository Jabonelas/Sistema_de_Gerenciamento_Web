using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Sistema_de_Gerenciamento_Web.Class;
using System.Linq;
using Sistema_de_Gerenciamento_Web.Class.Validacoes;
using Sistema_de_Gerenciamento_Web.Context;
using Sistema_de_Gerenciamento_Web.Models.ViewModel;

namespace Sistema_de_Gerenciamento_Web.Controllers.Cadastros
{
    public class CadastrosController : Controller
    {
        [HttpGet]
        public IActionResult CadastrarFornecedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarFornecedor(CadastroViewModel _cadastoViewModel)
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

                return View(_cadastoViewModel);
            }

            if (IsCpfExiste(_cadastoViewModel.registro.rg_cpf) == false || IsCnpjExiste(_cadastoViewModel.registro.rg_cnpj) == false)
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
                    }
                }
                catch (Exception)
                {
                    return BadRequest("Erro ao Inserir Dados do Fornecedor!");
                }

                ModelState.Clear();

                string mensagem = "Operação realizada com sucesso!";
                TempData["mensagemSucesso"] = mensagem;

                return View();
            }
            else
            {

                return View(_cadastoViewModel);
            }


            return View();


        }

        [HttpGet]
        public IActionResult CadastrarCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarCliente(CadastroClienteViewModel _cadastoViewModel)
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

                return View(_cadastoViewModel);
            }


            if (IsCpfExiste(_cadastoViewModel.registro.rg_cpf) == false || IsCnpjExiste(_cadastoViewModel.registro.rg_cnpj) == false)
            {
                try
                {
                    using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                    {
                        string tipoCadastro = "Cliente";

                        _cadastoViewModel.registro.rg_tipo_cadastro = tipoCadastro;

                        context.tb_enderecos.Add(_cadastoViewModel.endereco);
                        context.SaveChanges();

                        _cadastoViewModel.registro.fk_endereco = _cadastoViewModel.endereco.id_endereco;

                        context.tb_informacoes_comerciais.Add(_cadastoViewModel.InformacoesComercial);
                        context.SaveChanges();

                        _cadastoViewModel.registro.fk_informacao_comercial = _cadastoViewModel.InformacoesComercial.id_informacao_comercial;

                        context.tb_registro.Add(_cadastoViewModel.registro);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    return BadRequest("Erro ao Inserir Dados do Cliente!");
                }

                ModelState.Clear();

                string mensagem = "Operação realizada com sucesso!";
                TempData["mensagemSucesso"] = mensagem;

                return View();
            }
            else
            {

                return View(_cadastoViewModel);
            }

            return View();
        }

        [HttpPost]
        public JsonResult ValidarCPF(string _cpf)
        {
            bool isCpfValido = true;

            if (_cpf != null)
            {
                isCpfValido = ValidacaoCPF.IsCpfValido(_cpf);
            }

            return Json(isCpfValido);
        }

        [HttpPost]
        public JsonResult VerificacaoExistenciaCPF(string _cpf)
        {
            bool isCpfExiste = false;

            if (_cpf != null)
            {
                isCpfExiste = IsCpfExiste(_cpf);
            }

            return Json(isCpfExiste);
        }

        [HttpPost]
        public JsonResult VerificacaoExistenciaCNPJ(string _cnpj)
        {
            bool isCnpjExiste = false;

            if (_cnpj != null)
            {
                isCnpjExiste = IsCnpjExiste(_cnpj);
            }

            return Json(isCnpjExiste);
        }

        private bool IsCpfExiste(string _cpf)
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

        private bool IsCnpjExiste(string _cnpj)
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

        [HttpPost]
        public JsonResult ValidarRG(string _rg)
        {
            bool isRgValido = true;

            if (_rg != null)
            {
                isRgValido = ValidacaoRG.IsRGValido(_rg);
            }

            return Json(isRgValido);
        }

        [HttpPost]
        public JsonResult ValidarEmail(string _email)
        {
            bool isEmailValido = true;

            if (_email != null)
            {
                isEmailValido = ValidacaoEmail.IsEmailValido(_email);
            }

            return Json(isEmailValido);
        }

        [HttpPost]
        public JsonResult ValidarCNPJ(string _cnpj)
        {
            bool isCnpjValido = true;

            if (_cnpj != null)
            {
                isCnpjValido = ValidacaoCNPJ.IsCnpjValido(_cnpj);
            }

            return Json(isCnpjValido);
        }

        [HttpGet]
        [Route("Cadastros/GetCep/{cep}")]
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