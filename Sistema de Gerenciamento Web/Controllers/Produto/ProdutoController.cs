using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gerenciamento_Web.Class;
using Sistema_de_Gerenciamento_Web.Context;
using Sistema_de_Gerenciamento_Web.Models;

namespace Sistema_de_Gerenciamento_Web.Controllers.Produto
{
    public class ProdutoController : Controller
    {
        public IActionResult ProdutosCadastrados()
        {
            return View();
        }

        public IActionResult ConsultarEstoque()
        {
            return View();
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
                            pd_custo = item.Produto.pd_custo,
                            pd_margem = item.Produto.pd_margem,
                            pd_preco = item.Produto.pd_preco,
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
    }
}