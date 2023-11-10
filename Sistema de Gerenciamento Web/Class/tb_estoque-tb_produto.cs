using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gerenciamento_Web.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Gerenciamento_Web.Class
{
    public class tb_estoque_tb_produto
    {
        //tb_estoque

        public int id_estoque { get; set; }

        public decimal ep_quantidade { get; set; }

        public DateTime ep_data_entrada { get; set; }

        public DateTime? ep_data_exclusao { get; set; }

        public string ep_codigo_barras { get; set; }

        public string ep_status_situacao { get; set; }

        public int fk_produto { get; set; }

        public int fk_nota_fiscal_entrada { get; set; }

        //tb_produto

        public int id_produto { get; set; }

        public string pd_codigo { get; set; }

        public string pd_finalidade { get; set; }

        public string pd_nome { get; set; }

        public decimal pd_estoque_minimo { get; set; }

        public decimal pd_custo { get; set; }

        public decimal pd_margem { get; set; }

        public decimal pd_preco { get; set; }

        public string pd_observacoes { get; set; }

        public string pd_codigo_barras { get; set; }

        public string pd_tipo_produto { get; set; }

        public string pd_tipo_unidade { get; set; }

        public string pd_usuario_cadastrador { get; set; }

        public int fk_grupo { get; set; }

        public int? fk_registro_forncedor { get; set; }
    }
}