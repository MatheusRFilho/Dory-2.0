using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Dory2.Models
{
    public class Cadastro
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email), ErrorMessage = "Emails Diferentes")]
        [EmailAddress]
        public string ConfirmaEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número. Deve ser no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Required]
        [Compare(nameof(Senha), ErrorMessage = "Senhas Diferentes")]
        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número. Deve ser no mínimo 6 caracteres")]
        public string ConfirmaSenha { get; set; }
    }

    public class Acesso
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número. Deve ser no mínimo 6 caracteres")]
        public string Senha { get; set; }
    }

    public class EsqueceuSenha
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }

    public class RedefinirSenha
    {
        public string Email { get; set; }
        public string Hash { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número. Deve ser no mínimo 6 caracteres")]
        public string Senha { get; set; }
        [DataType(DataType.Password)]
        [Compare("Senha")]
        [Display(Name = "Confirma Senha")]
        public string ConfirmaSenha { get; set; }
    }

    public class UploadFoto
    {
        public string Foto { get; set; }
        public int PessoaId { get; set; }
    }

    public class EditarPerfil
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Contato { get; set; }

        [Required]
        [EnumDataType(typeof(Sexos))]
        public string Sexo { get; set; }

        public enum Sexos
        {
            Masculino = 0,
            Feminino = 1,
        }
    }

    public class FinishedRegister
    {
        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Rg { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Obrigatorio", AllowEmptyStrings = true)]
        public string Numero { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }


        public Ufs UF { get; set; }
        public enum Ufs
        {
            AC = 1,
            AL = 2,
            AP = 3,
            AM = 4,
            BA = 5,
            CE = 6,
            DF = 7,
            ES = 8,
            GO = 9,
            MA = 10,
            MT = 11,
            MS = 12,
            MG = 13,
            PA = 14,
            PB = 15,
            PR = 16,
            PE = 17,
            PI = 18,
            RJ = 19,
            RN = 20,
            RS = 21,
            RO = 22,
            RR = 23,
            SC = 24,
            SP = 25,
            SE = 26,
            TO = 27,
        }
    }
}