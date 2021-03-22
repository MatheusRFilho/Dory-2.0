namespace Dory2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Dory2.Models.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Dory2.Models.Contexto context)
        {
            context.Pessoa.AddOrUpdate(
             pessoa => pessoa.Id,
             new Models.Pessoa { 
                Id = 1, 
                Nome = "Admin", 
                Cpf = "111.111.111-11", 
                Cutis = "Branco", 
                Rg = "11.111.111-11", 
                Sexo = 'M', 
                DataNascimento = new DateTime(1990, 01, 23),
                Role = "0",
             }
            );
            context.Responsavel.AddOrUpdate(
             p => p.Email,
             new Models.Responsavel { Id = 1,  Email = "admin@admin.com.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", PessoaId = 1 }
            );
        }
    }
}
