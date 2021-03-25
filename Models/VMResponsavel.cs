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
}