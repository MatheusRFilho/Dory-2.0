using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Vulneravel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Observações")]
        [StringLength(50)]
        public string Observacoes { get; set; }
        [Required]
        [Display(Name ="Está Desaparecido ?")]
        public bool Status { get; set; }
        [Required]
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

    }
}