using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Tutorias
    {
        [Key]
        public int Id  { get; set; }
        [Required]
        public int ResponsavelId  { get; set; }
        [Required]
        public int PessoaId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Cadastro  { get; set; }
        [Required]
        public bool Ativo  { get; set; }

        public virtual Responsavel Responsavel { get; set; }
        public virtual Pessoa Pessoa { get; set; }

    }
}