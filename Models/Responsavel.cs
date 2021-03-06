using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Responsavel
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(30)]
        public string Email { get; set; }

        [Compare("Email")]
        [StringLength(30)]
        public string ConfirmEmail { get; set; }

        [Required]
        [Display(Name ="Senha")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8)]
        public string Senha { get; set; }

        [Compare("Senha")]
        [StringLength(15, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Celular")]
        [StringLength(14, MinimumLength = 14)]
        public string Celular { get; set; }

        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

    }
}