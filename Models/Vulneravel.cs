using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Vulneravel
    {
        public int Id { get; set; }
        public string Observacoes { get; set; }
        public bool Status { get; set; }
        public int IdPessoa { get; set; }

    }
}