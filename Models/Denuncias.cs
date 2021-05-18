using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Denuncias
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Digite uma breve descrição da denúncia")]
        [StringLength(255, MinimumLength = 10)]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataDenuncia { get; set; }
        [Required]
        public int DesaparecidoId { get; set; }
        public Desaparecido Desaparecido { get; set; }
    }
}