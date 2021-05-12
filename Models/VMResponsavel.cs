using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
}