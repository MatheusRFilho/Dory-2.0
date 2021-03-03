using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento  { get; set; }
        public char Sexo { get; set; }
        public string Cutis { get; set; }
        public int IdEndereco { get; set; }

}
}