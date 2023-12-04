﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_Gerenciamento_Web.Models
{
    public partial class tb_enderecos
    {
        public tb_enderecos()
        {
            tb_registro = new HashSet<tb_registro>();
        }

        [Key]
        public int id_endereco { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ed_tipo { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ed_cep { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ed_estado { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ed_locgradouro { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ed_numero { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ed_complemento { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ed_bairro { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ed_cidade { get; set; }

        [InverseProperty("fk_enderecoNavigation")]
        public virtual ICollection<tb_registro> tb_registro { get; set; }
    }
}