using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Casos
    {
        public int Id { get; set; }
        public string UltimasRoupas { get; set; }
        public string UltimaLocalizacao { get; set; }
        public string UltimoLugarVisto { get; set; }
        public string UltimoHorarioVisto { get; set; }
        public int IdDesaparecido { get; set; }
    }
}