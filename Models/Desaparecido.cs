using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Desaparecido
    {
        public int Id { get; set; }
        public bool Encontrado{ get; set; }
        public int IdPessoa { get; set; }
        public int IdVulneravel { get; set; }
    }
}