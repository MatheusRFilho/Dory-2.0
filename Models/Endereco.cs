using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Endereço")]
        [StringLength(30, MinimumLength = 6)]
        public string Logradouro { get; set; }
        [Required]
        [Display(Name = "Número")]
        [StringLength(7)]
        public string Numero { get; set; }
        [Required]
        [Display(Name = "Bairro")]
        [StringLength(30, MinimumLength = 6)]
        public string Bairro { get; set; }
        [Required]
        [Display(Name = "Cidade")]
        [StringLength(30)]
        public string Cidade { get; set; }
        [Required]
        [Display(Name = "Estado")]
        [StringLength(2)]
        public string Estado { get; set; }
    }
}