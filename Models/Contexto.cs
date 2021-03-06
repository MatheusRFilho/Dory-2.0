using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base(nameOrConnectionString: "StringConexao") { }

        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<Vulneravel> Vulneravel { get; set; }
        public DbSet<Desaparecido> Desaparecido { get; set; }
        public DbSet<Casos> Casos { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Mais_infos> Mais_Infos { get; set; }
        public DbSet<Tutorias> Tutorias { get; set; }

    }
}