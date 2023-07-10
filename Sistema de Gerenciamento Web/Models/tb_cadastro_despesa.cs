﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_Gerenciamento_Web.Models
{
    public partial class tb_cadastro_despesa
    {
        public tb_cadastro_despesa()
        {
            tb_repeticao_despesa = new HashSet<tb_repeticao_despesa>();
        }

        [Key]
        public int id_categoria_despesa { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string cd_categoria_agrupadora { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string cd_categoria { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string cd_tipo_custo { get; set; }

        [InverseProperty("fk_cadastro_despesaNavigation")]
        public virtual ICollection<tb_repeticao_despesa> tb_repeticao_despesa { get; set; }
    }
}