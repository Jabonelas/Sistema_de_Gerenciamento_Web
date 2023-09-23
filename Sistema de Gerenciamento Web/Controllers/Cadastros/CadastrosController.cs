using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Sistema_de_Gerenciamento_Web.Class;
using System.Linq;
using Sistema_de_Gerenciamento_Web.Class.Validacoes;
using Sistema_de_Gerenciamento_Web.Context;
using Sistema_de_Gerenciamento_Web.Models;
using Sistema_de_Gerenciamento_Web.Models.ViewModel;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Sistema_de_Gerenciamento_Web.Controllers.Cadastros
{
    public class CadastrosController : Controller
    {
        private Dictionary<int, string> dicNomeProdutos = new Dictionary<int, string>();

        private static int idProduto = 0;

        //public JsonResult BuscandoDadosProduto(string _nomeProduto)
        //{
        //    if (_nomeProduto != null || _nomeProduto != string.Empty)
        //    {
        //    }
        //    List<tb_produto> listaProduto = BuscarDadosProdutos(_nomeProduto);

        //    return Json(listaProduto);
        //}

        #region Produto

        public IActionResult DeletarProduto(int _idProduto, string mensagemErro = null)
        {
            try
            {
                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    var produtoDeletar = context.tb_produto.FirstOrDefault(x => x.id_produto.Equals(_idProduto));

                    if (produtoDeletar != null)
                    {
                        context.tb_produto.Remove(produtoDeletar);
                        //context.SaveChanges();

                        mensagemErro = "Realizado com sucesso.";
                    }
                    else
                    {
                        mensagemErro = "O produto não foi encontrado.";
                    }
                }

                ViewBag.MensagemErro = mensagemErro;

                return View("AlterarProduto");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao deletar o produto.";

                return View("AlterarProduto");
            }
        }

        public JsonResult BuscandoProdutoNome(string _nomeProduto)
        {
            if (_nomeProduto == null)
            {
                return Json(null);
            }

            BuscarProdutosNome(_nomeProduto);

            TempData["NomeProduto"] = null;
            TempData["NomeProduto"] = dicNomeProdutos;

            return Json(dicNomeProdutos);
        }

        private void BuscarProdutosNome(string _nomeProduto, string mensagemErro = null)
        {
            try
            {
                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    var dadosProduto = context.tb_produto
                        .Where(x => x.pd_nome
                            .Contains(_nomeProduto))
                        .Select(x => new tb_produto
                        {
                            pd_nome = x.pd_nome,
                            id_produto = x.id_produto,
                        }).ToList();

                    dicNomeProdutos.Clear();

                    if (dadosProduto != null)
                    {
                        foreach (var item in dadosProduto)
                        {
                            dicNomeProdutos.Add(item.id_produto, item.pd_nome);
                        }
                    }
                    else
                    {
                        mensagemErro = "O produto não foi encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar o produto.";
            }
        }

        //private tb_produto BuscarDadosProduto(string _nomeProduto, string mensagemErro = null)
        //{
        //    try
        //    {
        //        using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
        //        {
        //            var produtoEncontrado = context.tb_produto.FirstOrDefault(x => x.pd_nome.Equals(_nomeProduto));

        //            if (produtoEncontrado != null)
        //            {
        //                return produtoEncontrado;
        //            }
        //            else
        //            {
        //                mensagemErro = "O produto não foi encontrado.";
        //            }
        //        }

        //        ViewBag.MensagemErro = mensagemErro;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.MensagemErro = "Ocorreu um erro ao buscar produto.";
        //    }
        //}

        [HttpGet]
        public IActionResult AlterarProduto()
        {
            idProduto = 0;
            return View();
        }

        [HttpGet]
        public IActionResult AlterarProdutoPegandoId(int _idProduto)
        {
            idProduto = _idProduto;

            return View("AlterarProduto");
        }

        [HttpPost]
        public IActionResult AlterarProduto(string _nomeProduto)
        {
            if (_nomeProduto != null)
            {
                BuscarProdutosNome(_nomeProduto);

                TempData["NomeProduto"] = null;
                TempData["NomeProduto"] = dicNomeProdutos;

                TempData["LetrasDigitadas"] = null;
                TempData["LetrasDigitadas"] = _nomeProduto;

                TempData["DadosProduto"] = null;

                if (idProduto != 0)
                {
                    BuscarDadosProduto();
                }
            }

            return View();
        }

        public IActionResult AlterarDadosProduto(tb_produto _dadosProduto)
        {
            using (SistemaDeGerenciamento2_0Context contex = new SistemaDeGerenciamento2_0Context())
            {
                var dadoProduto = contex.tb_produto.FirstOrDefault(x => x.id_produto.Equals(_dadosProduto.id_produto));

                dadoProduto.pd_codigo = _dadosProduto.pd_codigo;
                dadoProduto.pd_finalidade = _dadosProduto.pd_finalidade;
                dadoProduto.pd_nome = _dadosProduto.pd_nome;
                dadoProduto.pd_estoque_minimo = _dadosProduto.pd_estoque_minimo;
                dadoProduto.pd_custo = _dadosProduto.pd_custo;
                dadoProduto.pd_margem = _dadosProduto.pd_margem;
                dadoProduto.pd_preco = _dadosProduto.pd_preco;
                dadoProduto.pd_observacoes = _dadosProduto.pd_observacoes;
                dadoProduto.pd_codigo_barras = _dadosProduto.pd_codigo_barras;
                dadoProduto.pd_tipo_produto = _dadosProduto.pd_tipo_produto;
                dadoProduto.pd_tipo_unidade = _dadosProduto.pd_tipo_unidade;

                contex.SaveChanges();
            }

            return View("AlterarProduto");
        }

        [HttpPost]
        public IActionResult CadastrarProduto(tb_produto _produto)
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

                PreencherCampos();

                return View(_produto);
            }

            if (IsCodigoProdutoExiste(_produto.pd_codigo) == false && IsCodigoBarrasProdutoExiste(_produto.pd_codigo_barras) == false)
            {
                try
                {
                    using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                    {
                        context.tb_produto.Add(_produto);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    return BadRequest("Erro ao Inserir Dados do Produto!");
                }

                ModelState.Clear();

                string mensagem = "Operação realizada com sucesso!";
                TempData["mensagemSucesso"] = mensagem;

                PreencherCampos();

                return View();
            }
            else
            {
                PreencherCampos();

                return View(_produto);
            }

            PreencherCampos();

            return View();
        }

        private void PreencherCampos()
        {
            BuscarListaGrupoProdutosCadastrados();

            BuscarListaFornecedoresCadastrados();
        }

        private bool IsCodigoBarrasProdutoExiste(string _codigoBarrasProduto)
        {
            using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
            {
                var isProdutoExiste = context.tb_produto.Where(x => x.pd_codigo_barras.Equals(_codigoBarrasProduto)).Any();

                if (isProdutoExiste == true)
                {
                    string mensagem = "Código do Produto já Cadastrado!";
                    TempData["mensagemFalha"] = mensagem;
                }

                return isProdutoExiste;
            }
        }

        private bool IsCodigoProdutoExiste(string _codigoProduto)
        {
            using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
            {
                var isProdutoExiste = context.tb_produto.Where(x => x.pd_codigo.Equals(_codigoProduto)).Any();

                if (isProdutoExiste == true)
                {
                    string mensagem = "Código de Barras já Cadastrado!";
                    TempData["mensagemFalha"] = mensagem;
                }

                return isProdutoExiste;
            }
        }

        [HttpPost]
        public JsonResult VerificacaoExistenciaCodigoBarras(string _codigoBarras)
        {
            bool isCodigoBarrasExiste = false;

            if (_codigoBarras != null)
            {
                isCodigoBarrasExiste = IsCodigoBarrasProdutoExiste(_codigoBarras);
            }

            return Json(isCodigoBarrasExiste);
        }

        [HttpPost]
        public JsonResult VerificacaoExistenciaCodigoProduto(string _codigoProduto)
        {
            bool isCodigoProduto = false;

            if (_codigoProduto != null)
            {
                isCodigoProduto = IsCodigoProdutoExiste(_codigoProduto);
            }

            return Json(isCodigoProduto);
        }

        [HttpGet]
        public IActionResult CadastrarProduto()
        {
            PreencherCampos();

            return View();
        }

        private void BuscarListaGrupoProdutosCadastrados()
        {
            //try
            //{
            using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
            {
                var grupo = context.tb_grupo.Where(x => x.gp_nome_grupo != null && x.gp_nome_agrupador != null).ToList();

                ViewData["GruposCadastrados"] = null;
                ViewData["GruposCadastrados"] = grupo;
            }
            //}
            //catch (Exception)
            //{
            //    return BadRequest("Erro ao Buscar Dados do Grupos Cadastrados!");
            //}
        }

        #endregion Produto

        private void BuscarListaFornecedoresCadastrados()
        {
            //try
            //{
            using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
            {
                var fornecedores = context.tb_registro.Where(x => x.rg_tipo_cadastro == "Fornecedor").ToList();

                ViewData["FornecedoresCadastrados"] = null;
                ViewData["FornecedoresCadastrados"] = fornecedores;
            }
            //}
            //catch (Exception)
            //{
            //    return BadRequest("Erro ao Buscar Dados do Fornecedores Cadastrados!");
            //}
        }

        [HttpGet]
        public IActionResult CadastrarFuncionario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarFuncionario(CadastroViewModel _cadastoViewModel)
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
                        string tipoCadastro = "Funcionario";

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
                    return BadRequest("Erro ao Inserir Dados do Funcionario!");
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

        //public PartialViewResult ExibirPartialView(int _idProduto)
        //{
        //    BuscarDadosProduto(_idProduto);

        //    return PartialView("AlterarDadosProdutoPartialView");
        //}

        private void BuscarDadosProduto()
        {
            using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
            {
                var dadosProdutos = context.tb_produto.FirstOrDefault(x => x.id_produto.Equals(idProduto));

                TempData["DadosProduto"] = null;
                TempData["DadosProduto"] = dadosProdutos;

                idProduto = 0;
            }
        }
    }
}