using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Responsavel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int IdPessoa { get; set; }

    }
}