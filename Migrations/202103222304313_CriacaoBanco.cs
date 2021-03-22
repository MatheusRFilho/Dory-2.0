namespace Dory2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Casos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UltimasRoupas = c.String(maxLength: 100, storeType: "nvarchar"),
                        UltimaLocalizacao = c.String(maxLength: 100, storeType: "nvarchar"),
                        UltimoLugarVisto = c.String(maxLength: 100, storeType: "nvarchar"),
                        UltimoHorarioVisto = c.String(maxLength: 100, storeType: "nvarchar"),
                        DesaparecidoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Desaparecidoes", t => t.DesaparecidoId, cascadeDelete: true)
                .Index(t => t.DesaparecidoId);
            
            CreateTable(
                "dbo.Desaparecidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Encontrado = c.DateTime(nullable: false, precision: 0),
                        PessoaId = c.Int(nullable: false),
                        VulneravelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoas", t => t.PessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Vulneravels", t => t.VulneravelId, cascadeDelete: true)
                .Index(t => t.PessoaId)
                .Index(t => t.VulneravelId);
            
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cpf = c.String(unicode: false),
                        Rg = c.String(unicode: false),
                        Nome = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        DataNascimento = c.DateTime(nullable: false, precision: 0),
                        Cutis = c.String(unicode: false),
                        EnderecoId = c.Int(nullable: false),
                        Role = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enderecoes", t => t.EnderecoId, cascadeDelete: true)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Enderecoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(nullable: false, unicode: false),
                        Numero = c.String(nullable: false, unicode: false),
                        Bairro = c.String(nullable: false, unicode: false),
                        Cidade = c.String(nullable: false, unicode: false),
                        Estado = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vulneravels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Observacoes = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Status = c.Boolean(nullable: false),
                        PessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoas", t => t.PessoaId, cascadeDelete: true)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.Contatoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeContato = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Tipo = c.String(nullable: false, unicode: false),
                        ResponsavelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Responsavels", t => t.ResponsavelId, cascadeDelete: true)
                .Index(t => t.ResponsavelId);
            
            CreateTable(
                "dbo.Responsavels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Senha = c.String(nullable: false, unicode: false),
                        Celular = c.String(unicode: false),
                        PessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoas", t => t.PessoaId, cascadeDelete: true)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.Mais_infos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cabelo = c.String(unicode: false),
                        Olhos = c.String(unicode: false),
                        Altura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descricao = c.String(unicode: false),
                        TipoSanguineo = c.String(unicode: false),
                        DeficienciaMental = c.String(unicode: false),
                        DeficienciaFisica = c.String(unicode: false),
                        RestricaoAlimentar = c.String(unicode: false),
                        RestricaoMedicamentos = c.String(unicode: false),
                        Doencas = c.String(unicode: false),
                        DesaparecidoId = c.String(unicode: false),
                        VulneravelId = c.String(unicode: false),
                        Desaparecido_Id = c.Int(),
                        Vulneravel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Desaparecidoes", t => t.Desaparecido_Id)
                .ForeignKey("dbo.Vulneravels", t => t.Vulneravel_Id)
                .Index(t => t.Desaparecido_Id)
                .Index(t => t.Vulneravel_Id);
            
            CreateTable(
                "dbo.Tutorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResponsavelId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        Cadastro = c.DateTime(nullable: false, precision: 0),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoas", t => t.PessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Responsavels", t => t.ResponsavelId, cascadeDelete: true)
                .Index(t => t.ResponsavelId)
                .Index(t => t.PessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tutorias", "ResponsavelId", "dbo.Responsavels");
            DropForeignKey("dbo.Tutorias", "PessoaId", "dbo.Pessoas");
            DropForeignKey("dbo.Mais_infos", "Vulneravel_Id", "dbo.Vulneravels");
            DropForeignKey("dbo.Mais_infos", "Desaparecido_Id", "dbo.Desaparecidoes");
            DropForeignKey("dbo.Responsavels", "PessoaId", "dbo.Pessoas");
            DropForeignKey("dbo.Contatoes", "ResponsavelId", "dbo.Responsavels");
            DropForeignKey("dbo.Casos", "DesaparecidoId", "dbo.Desaparecidoes");
            DropForeignKey("dbo.Desaparecidoes", "VulneravelId", "dbo.Vulneravels");
            DropForeignKey("dbo.Vulneravels", "PessoaId", "dbo.Pessoas");
            DropForeignKey("dbo.Desaparecidoes", "PessoaId", "dbo.Pessoas");
            DropForeignKey("dbo.Pessoas", "EnderecoId", "dbo.Enderecoes");
            DropIndex("dbo.Tutorias", new[] { "PessoaId" });
            DropIndex("dbo.Tutorias", new[] { "ResponsavelId" });
            DropIndex("dbo.Mais_infos", new[] { "Vulneravel_Id" });
            DropIndex("dbo.Mais_infos", new[] { "Desaparecido_Id" });
            DropIndex("dbo.Responsavels", new[] { "PessoaId" });
            DropIndex("dbo.Contatoes", new[] { "ResponsavelId" });
            DropIndex("dbo.Vulneravels", new[] { "PessoaId" });
            DropIndex("dbo.Pessoas", new[] { "EnderecoId" });
            DropIndex("dbo.Desaparecidoes", new[] { "VulneravelId" });
            DropIndex("dbo.Desaparecidoes", new[] { "PessoaId" });
            DropIndex("dbo.Casos", new[] { "DesaparecidoId" });
            DropTable("dbo.Tutorias");
            DropTable("dbo.Mais_infos");
            DropTable("dbo.Responsavels");
            DropTable("dbo.Contatoes");
            DropTable("dbo.Vulneravels");
            DropTable("dbo.Enderecoes");
            DropTable("dbo.Pessoas");
            DropTable("dbo.Desaparecidoes");
            DropTable("dbo.Casos");
        }
    }
}
