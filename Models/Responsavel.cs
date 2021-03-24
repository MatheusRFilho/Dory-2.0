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

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Celular { get; set; }

        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Contato> Contato { get; set; }

    }
}