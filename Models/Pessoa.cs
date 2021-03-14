using System;
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

        [Display(Name = "CPF")]
        [StringLength(15, MinimumLength = 15)]
        public string Cpf { get; set; }

        [Display(Name = "RG")]
        [StringLength(12, MinimumLength = 12)]
        public string Rg { get; set; }

        [Required]
        [Display(Name ="Nome")]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [Display(Name ="Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento  { get; set; }

        [Required]
        [Display(Name ="Sexo")]
        public char Sexo { get; set; }

        [Display(Name = "Etnia")]
        public string Cutis { get; set; }

        public int EnderecoId { get; set; }

        [Required]
        public bool isAdm { get; set; }

        public virtual Endereco Endereco { get; set; }

    }
}