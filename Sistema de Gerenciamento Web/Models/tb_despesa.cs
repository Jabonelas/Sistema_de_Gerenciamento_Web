﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_Gerenciamento_Web.Models
{
    public partial class tb_despesa
    {
        [Key]
        public int id_despesa { get; set; }
        [Column(TypeName = "date")]
        public DateTime dp_data { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string dp_observacao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal dp_sub_valor_total { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? dp_desconto { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? dp_juros { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? dp_multa { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? dp_valor_lancamento { get; set; }
        [Column(TypeName = "date")]
        public DateTime? dp_pagamento_em { get; set; }
        [Column(TypeName = "date")]
        public DateTime? dp_vencimento { get; set; }
        public int? dp_parcelas { get; set; }
        [Column(TypeName = "image")]
        public byte[] dp_imagem { get; set; }
        public bool? dp_repeticao { get; set; }
        public int fk_registro { get; set; }
        public int? fk_repeticao_despesa { get; set; }

        [ForeignKey("fk_registro")]
        [InverseProperty("tb_despesa")]
        public virtual tb_registro fk_registroNavigation { get; set; }
        [ForeignKey("fk_repeticao_despesa")]
        [InverseProperty("tb_despesa")]
        public virtual tb_repeticao_despesa fk_repeticao_despesaNavigation { get; set; }
    }
}