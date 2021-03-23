using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Casos
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Últimas Roupas que a pessoa estava")]
        [StringLength(100, MinimumLength = 6)]
        public string UltimaRoupa { get; set; }
        [Display(Name = "Últimas Localização da pessoa")]
        [StringLength(100, MinimumLength = 6)]
        public string UltimaLocalizacao { get; set; }
        [Display(Name = "Último lugar visto")]
        [StringLength(100, MinimumLength = 6)]
        public string UltimoLugarVisto { get; set; }
        [Display(Name = "Último horário visto")]
        [StringLength(100, MinimumLength = 6)]
        public string UltimoHorarioVisto { get; set; }
        [Required]
        public int DesaparecidoId { get; set; }
        public virtual Desaparecido Desaparecido { get; set; } 
    }
}