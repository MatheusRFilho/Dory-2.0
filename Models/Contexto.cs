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
        public DbSet<Mais_infos> Mais_Infos { get; set; }
        public DbSet<Tutorias> Tutorias { get; set; }
        public DbSet<Contato> Contato { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            var end = mb.Entity<Endereco>();
            end.ToTable("end_endereco");
            end.Property(x => x.Id).HasColumnName("end_codigo");
            end.Property(x => x.Bairro).HasColumnName("end_bairro");
            end.Property(x => x.Cidade).HasColumnName("end_cidade");
            end.Property(x => x.Estado).HasColumnName("end_estado");
            end.Property(x => x.Logradouro).HasColumnName("end_logradouro");
            end.Property(x => x.Numero).HasColumnName("end_numero");

            var pes = mb.Entity<Pessoa>();
            pes.ToTable("pes_pessoa");
            pes.Property(x => x.Id).HasColumnName("pes_codigo");
            pes.Property(x => x.Cpf).HasColumnName("pes_cpf");
            pes.Property(x => x.Rg).HasColumnName("pes_rg");
            pes.Property(x => x.Nome).HasColumnName("pes_nome");
            pes.Property(x => x.DataNascimento).HasColumnName("pes_nascimento");
            pes.Property(x => x.Sexo).HasColumnName("pes_sexo");
            pes.Property(x => x.Cutis).HasColumnName("pes_cutis");
            pes.Property(x => x.Tipo).HasColumnName("pes_tipo");
            pes.Property(x => x.EnderecoId).HasColumnName("end_codigo");

            var res = mb.Entity<Responsavel>();
            res.ToTable("res_responsavel");
            res.Property(x => x.Id).HasColumnName("res_codigo");
            res.Property(x => x.Celular).HasColumnName("res_celular");
            res.Property(x => x.Email).HasColumnName("res_email");
            res.Property(x => x.Senha).HasColumnName("res_senha");
            res.Property(x => x.PessoaId).HasColumnName("pes_codigo");

            var vul = mb.Entity<Vulneravel>();
            vul.ToTable("vul_vulneravel");
            vul.Property(x => x.Id).HasColumnName("vul_codigo");
            vul.Property(x => x.Observacoes).HasColumnName("vul_observacoes");
            vul.Property(x => x.Status).HasColumnName("vul_status");
            vul.Property(x => x.PessoaId).HasColumnName("pes_codigo");

            var des = mb.Entity<Desaparecido>();
            des.ToTable("des_desaparecido");
            des.Property(x => x.Id).HasColumnName("des_codigo");
            des.Property(x => x.Encontrado).HasColumnName("des_encontrado");
            des.Property(x => x.PessoaId).HasColumnName("pes_codigo");
            des.Property(x => x.VulneravelId).HasColumnName("vul_codigo");

            var cas = mb.Entity<Casos>();
            cas.ToTable("cas_casos");
            cas.Property(x => x.Id).HasColumnName("cas_codigo");
            cas.Property(x => x.UltimaLocalizacao).HasColumnName("cas_ultima_localizacao");
            cas.Property(x => x.UltimaRoupa).HasColumnName("cas_ultima_roupa");
            cas.Property(x => x.UltimoHorarioVisto).HasColumnName("cas_ultimo_horario");
            cas.Property(x => x.UltimoLugarVisto).HasColumnName("cas_ultimo_lugar");
            cas.Property(x => x.DesaparecidoId).HasColumnName("des_codigo");

            var min = mb.Entity<Mais_infos>();
            min.ToTable("min_mais_informacoes");
            min.Property(x => x.Id).HasColumnName("min_codigo");
            min.Property(x => x.Cabelo).HasColumnName("min_cabelo");
            min.Property(x => x.Olhos).HasColumnName("min_olhos");
            min.Property(x => x.Altura).HasColumnName("min_altura");
            min.Property(x => x.Peso).HasColumnName("min_peso");
            min.Property(x => x.Descricao).HasColumnName("min_descricao");
            min.Property(x => x.TipoSanguineo).HasColumnName("min_tipo_sanguineo");
            min.Property(x => x.DeficienciaMental).HasColumnName("min_deficiencia_mental");
            min.Property(x => x.DeficienciaFisica).HasColumnName("min_deficiencia_fisica");
            min.Property(x => x.RestricaoAlimentar).HasColumnName("min_restricao_alimentar");
            min.Property(x => x.RestricaoMedicamentos).HasColumnName("min_restricao_medicamentos");
            min.Property(x => x.Doencas).HasColumnName("min_doencas");
            min.Property(x => x.DesaparecidoId).HasColumnName("des_codigo");
            min.Property(x => x.VulneravelId).HasColumnName("vul_codigo");

            var tut = mb.Entity<Tutorias>();
            tut.ToTable("tut_tutorias");
            tut.Property(x => x.Id).HasColumnName("tut_codigo");
            tut.Property(x => x.Ativo).HasColumnName("tut_ativo");
            tut.Property(x => x.Cadastro).HasColumnName("tut_data_cadastro");
            tut.Property(x => x.PessoaId).HasColumnName("pes_codigo");
            tut.Property(x => x.ResponsavelId).HasColumnName("res_codigo");

            var con = mb.Entity<Contato>();
            con.ToTable("cont_contatos");
            con.Property(x => x.Id).HasColumnName("con_codigo");
            con.Property(x => x.NomeContato).HasColumnName("con_nome_contato");
            con.Property(x => x.Tipo).HasColumnName("con_tipo");
            con.Property(x => x.Numero).HasColumnName("con_numero");
            con.Property(x => x.ResponsavelId).HasColumnName("res_codigo");
        }
    }
}