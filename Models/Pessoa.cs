﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento  { get; set; }

        [Required]
        public string Sexo { get; set; }

        public string Cutis { get; set; }

        public int EnderecoId { get; set; }

        [Required]
        [EnumDataType(typeof(Tipos))]
        public string Tipo { get; set; }
        public enum Tipos
        {
            Admin = 0,
            Comum = 1,
            Pago = 2,
        }

        public virtual Endereco Endereco { get; set; }

    }
}