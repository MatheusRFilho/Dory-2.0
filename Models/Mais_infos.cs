using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Mais_infos
    {
        [Key]
        public int Id { get; set; }
        public string Cabelo { get; set; }
        public string Olhos { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public string Descricao { get; set; }
        public string TipoSanguineo { get; set; }
        public string DeficienciaMental { get; set; }
        public string DeficienciaFisica { get; set; }
        public string RestricaoAlimentar { get; set; }
        public string RestricaoMedicamentos { get; set; }
        public string Doencas { get; set; }

        public int? DesaparecidoId { get; set; }
        public int? VulneravelId { get; set; }

        public virtual Desaparecido Desaparecido { get; set; }
        public virtual Vulneravel Vulneravel { get; set; }

    }
}