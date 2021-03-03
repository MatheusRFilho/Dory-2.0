using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string NomeContato { get; set; }
        public string Tipo { get; set; }
        public int IdPessoa{ get; set; }

    }
}