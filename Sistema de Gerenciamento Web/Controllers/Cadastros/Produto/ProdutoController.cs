using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gerenciamento_Web.Class;
using Sistema_de_Gerenciamento_Web.Context;
using Sistema_de_Gerenciamento_Web.Models;

namespace Sistema_de_Gerenciamento_Web.Controllers.Cadastros.Produto
{
    public class ProdutoController : Controller
    {
        private Dictionary<int, string> dicNomeProdutos = new Dictionary<int, string>();

        private static int idProduto = 0;

        public IActionResult ProdutosCadastrados()
        {
            return View();
        }

        public IActionResult ConsultarEstoque()
        {
            return View();
        }

        public IActionResult DeletarProduto(int _idProduto, string mensagemErro = null)
        {
            try
            {
                if (_idProduto != 0)
                {
                    using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                    {
                        var produtoDeletar = context.tb_produto.FirstOrDefault(x => x.id_produto.Equals(_idProduto));

                        context.tb_produto.Remove(produtoDeletar);
                        context.SaveChanges();
                    }
                }
                string mensagem = "Produto excluido com sucesso!";
                TempData["mensagemSucesso"] = mensagem;

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

        public List<tb_produto> BuscarTodosProdutosCadastrados()
        {
            try
            {
                List<tb_produto> produtos;

                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    produtos = context.tb_produto.ToList();
                }

                return produtos;
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar produtos cadastrados.";

                return null;
            }
        }

        public List<tb_estoque_tb_produto> BuscarEstoqueProdutos()
        {
            try
            {
                List<tb_estoque_tb_produto> estoque = new List<tb_estoque_tb_produto>();

                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    var dadosEstoque = context.tb_produto
                    .Join(context.tb_estoque,
                    produto => produto.id_produto,
                    estoque => estoque.fk_produto,
                    (produto, estoque) => new
                    {
                        Estoque = estoque,
                        Produto = produto
                    })
                    .Where(x => x.Produto.id_produto == x.Estoque.fk_produto)
                    .ToList();

                    foreach (var item in dadosEstoque)
                    {
                        estoque.Add(new tb_estoque_tb_produto
                        {
                            id_produto = item.Produto.id_produto,
                            pd_nome = item.Produto.pd_nome,
                            pd_codigo = item.Produto.pd_codigo,
                            ep_codigo_barras = item.Produto.pd_codigo_barras,
                            ep_status_situacao = item.Estoque.ep_status_situacao,
                            ep_quantidade = item.Estoque.ep_quantidade,
                            pd_tipo_unidade = item.Produto.pd_tipo_unidade,
                            pd_estoque_minimo = item.Produto.pd_estoque_minimo,
                        });
                    }
                }

                return estoque;
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar estoque produtos.";
                return null;
            }
        }

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
            try
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

                    string mensagem = "Operação realizada com sucesso!";
                    TempData["mensagemSucesso"] = mensagem;
                }

                return View("AlterarProduto");
            }
            catch (Exception x)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao alterar o produto.";

                return View("AlterarProduto");
            }
        }

        [HttpPost]
        public IActionResult CadastrarProduto(tb_produto _produto)
        {
            if (!IsCamposPreenchidos())
            {
                PreencherCampos();

                return View(_produto);
            }

            if (IsCodigoProdutoExiste(_produto.pd_codigo) == false && IsCodigoBarrasProdutoExiste(_produto.pd_codigo_barras) == false)
            {
                InserirCadastroProduto(_produto);

                return View();
            }
            else
            {
                PreencherCampos();

                return View(_produto);
            }
        }

        private void InserirCadastroProduto(tb_produto _produto)
        {
            try
            {
                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    context.tb_produto.Add(_produto);
                    context.SaveChanges();
                }

                ModelState.Clear();

                string mensagem = "Operação realizada com sucesso!";
                TempData["mensagemSucesso"] = mensagem;

                PreencherCampos();
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao cadastrar o produto.";
            }
        }

        private bool IsCodigoBarrasProdutoExiste(string _codigoBarrasProduto)
        {
            try
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
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao verificar codigo barras do produto.";

                return false;
            }
        }

        private bool IsCodigoProdutoExiste(string _codigoProduto)
        {
            try
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
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao verificar codigo do produto.";

                return false;
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
            try
            {
                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    var grupo = context.tb_grupo.Where(x => x.gp_nome_grupo != null && x.gp_nome_agrupador != null).ToList();

                    ViewData["GruposCadastrados"] = null;
                    ViewData["GruposCadastrados"] = grupo;
                }
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar lista de grupo dos produto cadastrados.";
            }
        }

        private void PreencherCampos()
        {
            BuscarListaGrupoProdutosCadastrados();

            BuscarListaFornecedoresCadastrados();
        }

        private void BuscarListaFornecedoresCadastrados()
        {
            try
            {
                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    var fornecedores = context.tb_registro.Where(x => x.rg_tipo_cadastro == "Fornecedor").ToList();

                    ViewData["FornecedoresCadastrados"] = null;
                    ViewData["FornecedoresCadastrados"] = fornecedores;
                }
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar lista de grupo dos fornecedores cadastrados.";
            }
        }

        private void BuscarDadosProduto()
        {
            try
            {
                using (SistemaDeGerenciamento2_0Context context = new SistemaDeGerenciamento2_0Context())
                {
                    var dadosProdutos = context.tb_produto.FirstOrDefault(x => x.id_produto.Equals(idProduto));

                    TempData["DadosProduto"] = null;
                    TempData["DadosProduto"] = dadosProdutos;

                    idProduto = 0;
                }
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Ocorreu um erro ao buscar dados produto";
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
    }
}