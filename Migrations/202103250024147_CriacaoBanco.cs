namespace Dory2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cas_casos",
                c => new
                    {
                        cas_codigo = c.Int(nullable: false, identity: true),
                        cas_ultima_roupa = c.String(maxLength: 100, storeType: "nvarchar"),
                        cas_ultima_localizacao = c.String(maxLength: 100, storeType: "nvarchar"),
                        cas_ultimo_lugar = c.String(maxLength: 100, storeType: "nvarchar"),
                        cas_ultimo_horario = c.String(maxLength: 100, storeType: "nvarchar"),
                        des_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cas_codigo)
                .ForeignKey("dbo.des_desaparecido", t => t.des_codigo, cascadeDelete: true)
                .Index(t => t.des_codigo);
            
            CreateTable(
                "dbo.des_desaparecido",
                c => new
                    {
                        des_codigo = c.Int(nullable: false, identity: true),
                        des_encontrado = c.DateTime(nullable: false, precision: 0),
                        pes_codigo = c.Int(nullable: false),
                        vul_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.des_codigo)
                .ForeignKey("dbo.pes_pessoa", t => t.pes_codigo, cascadeDelete: true)
                .ForeignKey("dbo.vul_vulneravel", t => t.vul_codigo, cascadeDelete: true)
                .Index(t => t.pes_codigo)
                .Index(t => t.vul_codigo);
            
            CreateTable(
                "dbo.pes_pessoa",
                c => new
                    {
                        pes_codigo = c.Int(nullable: false, identity: true),
                        pes_cpf = c.String(unicode: false),
                        pes_rg = c.String(unicode: false),
                        pes_nome = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        pes_nascimento = c.DateTime(nullable: false, precision: 0),
                        pes_sexo = c.String(unicode: false),
                        pes_cutis = c.String(unicode: false),
                        pes_tipo = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.pes_codigo);
            
            CreateTable(
                "dbo.vul_vulneravel",
                c => new
                    {
                        vul_codigo = c.Int(nullable: false, identity: true),
                        vul_observacoes = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        vul_status = c.Boolean(nullable: false),
                        pes_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.vul_codigo)
                .ForeignKey("dbo.pes_pessoa", t => t.pes_codigo, cascadeDelete: true)
                .Index(t => t.pes_codigo);
            
            CreateTable(
                "dbo.cont_contatos",
                c => new
                    {
                        con_codigo = c.Int(nullable: false, identity: true),
                        con_nome_contato = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        con_tipo = c.String(nullable: false, unicode: false),
                        con_numero = c.String(nullable: false, unicode: false),
                        res_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.con_codigo)
                .ForeignKey("dbo.res_responsavel", t => t.res_codigo, cascadeDelete: true)
                .Index(t => t.res_codigo);
            
            CreateTable(
                "dbo.res_responsavel",
                c => new
                    {
                        res_codigo = c.Int(nullable: false, identity: true),
                        res_email = c.String(unicode: false),
                        res_senha = c.String(unicode: false),
                        res_celular = c.String(unicode: false),
                        pes_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.res_codigo)
                .ForeignKey("dbo.pes_pessoa", t => t.pes_codigo, cascadeDelete: true)
                .Index(t => t.pes_codigo);
            
            CreateTable(
                "dbo.end_endereco",
                c => new
                    {
                        end_codigo = c.Int(nullable: false, identity: true),
                        end_logradouro = c.String(nullable: false, unicode: false),
                        end_numero = c.String(nullable: false, unicode: false),
                        end_bairro = c.String(nullable: false, unicode: false),
                        end_cidade = c.String(nullable: false, unicode: false),
                        end_estado = c.String(nullable: false, unicode: false),
                        pes_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.end_codigo)
                .ForeignKey("dbo.pes_pessoa", t => t.pes_codigo, cascadeDelete: true)
                .Index(t => t.pes_codigo);
            
            CreateTable(
                "dbo.min_mais_informacoes",
                c => new
                    {
                        min_codigo = c.Int(nullable: false, identity: true),
                        min_cabelo = c.String(unicode: false),
                        min_olhos = c.String(unicode: false),
                        min_altura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        min_peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        min_descricao = c.String(unicode: false),
                        min_tipo_sanguineo = c.String(unicode: false),
                        min_deficiencia_mental = c.String(unicode: false),
                        min_deficiencia_fisica = c.String(unicode: false),
                        min_restricao_alimentar = c.String(unicode: false),
                        min_restricao_medicamentos = c.String(unicode: false),
                        min_doencas = c.String(unicode: false),
                        des_codigo = c.Int(nullable: false),
                        vul_codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.min_codigo)
                .ForeignKey("dbo.des_desaparecido", t => t.des_codigo, cascadeDelete: true)
                .ForeignKey("dbo.vul_vulneravel", t => t.vul_codigo, cascadeDelete: true)
                .Index(t => t.des_codigo)
                .Index(t => t.vul_codigo);
            
            CreateTable(
                "dbo.tut_tutorias",
                c => new
                    {
                        tut_codigo = c.Int(nullable: false, identity: true),
                        res_codigo = c.Int(nullable: false),
                        pes_codigo = c.Int(nullable: false),
                        tut_data_cadastro = c.DateTime(nullable: false, precision: 0),
                        tut_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tut_codigo)
                .ForeignKey("dbo.pes_pessoa", t => t.pes_codigo, cascadeDelete: true)
                .ForeignKey("dbo.res_responsavel", t => t.res_codigo, cascadeDelete: true)
                .Index(t => t.res_codigo)
                .Index(t => t.pes_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tut_tutorias", "res_codigo", "dbo.res_responsavel");
            DropForeignKey("dbo.tut_tutorias", "pes_codigo", "dbo.pes_pessoa");
            DropForeignKey("dbo.min_mais_informacoes", "vul_codigo", "dbo.vul_vulneravel");
            DropForeignKey("dbo.min_mais_informacoes", "des_codigo", "dbo.des_desaparecido");
            DropForeignKey("dbo.end_endereco", "pes_codigo", "dbo.pes_pessoa");
            DropForeignKey("dbo.res_responsavel", "pes_codigo", "dbo.pes_pessoa");
            DropForeignKey("dbo.cont_contatos", "res_codigo", "dbo.res_responsavel");
            DropForeignKey("dbo.cas_casos", "des_codigo", "dbo.des_desaparecido");
            DropForeignKey("dbo.des_desaparecido", "vul_codigo", "dbo.vul_vulneravel");
            DropForeignKey("dbo.vul_vulneravel", "pes_codigo", "dbo.pes_pessoa");
            DropForeignKey("dbo.des_desaparecido", "pes_codigo", "dbo.pes_pessoa");
            DropIndex("dbo.tut_tutorias", new[] { "pes_codigo" });
            DropIndex("dbo.tut_tutorias", new[] { "res_codigo" });
            DropIndex("dbo.min_mais_informacoes", new[] { "vul_codigo" });
            DropIndex("dbo.min_mais_informacoes", new[] { "des_codigo" });
            DropIndex("dbo.end_endereco", new[] { "pes_codigo" });
            DropIndex("dbo.res_responsavel", new[] { "pes_codigo" });
            DropIndex("dbo.cont_contatos", new[] { "res_codigo" });
            DropIndex("dbo.vul_vulneravel", new[] { "pes_codigo" });
            DropIndex("dbo.des_desaparecido", new[] { "vul_codigo" });
            DropIndex("dbo.des_desaparecido", new[] { "pes_codigo" });
            DropIndex("dbo.cas_casos", new[] { "des_codigo" });
            DropTable("dbo.tut_tutorias");
            DropTable("dbo.min_mais_informacoes");
            DropTable("dbo.end_endereco");
            DropTable("dbo.res_responsavel");
            DropTable("dbo.cont_contatos");
            DropTable("dbo.vul_vulneravel");
            DropTable("dbo.pes_pessoa");
            DropTable("dbo.des_desaparecido");
            DropTable("dbo.cas_casos");
        }
    }
}
