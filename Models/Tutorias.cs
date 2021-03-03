using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Tutorias
    {
        public int Id  { get; set; }
        public int IdResponsavel  { get; set; }
        public int IdPessoa  { get; set; }
        public DateTime Cadastro  { get; set; }
        public bool Ativo  { get; set; }

}
}