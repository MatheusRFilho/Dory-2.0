using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nome do Contato")]
        [StringLength(20)]
        public string NomeContato { get; set; }
        [Required]
        [Display(Name = "Tipo do Contato")]
        [EnumDataType(typeof (Tipos))]
        public string Tipo { get; set; }
        public enum Tipos
        {
            Residencial = 0,
            Celular = 1,
            Comercial = 2,
        }
        [Required]
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }


    }
}