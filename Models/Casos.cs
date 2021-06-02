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
        public string UltimaRoupa { get; set; }
        public string UltimaLocalizacao { get; set; }
        public string UltimoLugarVisto { get; set; }
        public string UltimoHorarioVisto { get; set; }
        public string ContatoQuemViu { get; set; }
        public string EmailQuemViu { get; set; }
        public string NomeQuemViu { get; set; }
        public int DesaparecidoId { get; set; }
        public virtual Desaparecido Desaparecido { get; set; } 
    }
}