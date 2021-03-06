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
        [Display(Name = "Cor do cabelo")]
        [StringLength(20)]
        public string Cabelo { get; set; }
        [Display(Name = "Cor dos olhos")]
        [StringLength(20)]
        public string Olhos { get; set; }
        [Display(Name = "Altura")]
        public decimal Altura { get; set; }
        [Display(Name = "Peso")]
        public decimal Peso { get; set; }
        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 10)]
        public string Descricao { get; set; }
        [Display(Name = "Tipo sanguineo")]
        public string TipoSanguineo { get; set; }
        [Display(Name = "Descreva aqui")]
        [StringLength(50, MinimumLength = 6)]
        public string DeficienciaMental { get; set; }
        [Display(Name = "Descreva aqui")]
        [StringLength(50, MinimumLength = 6)]
        public string DeficienciaFisica { get; set; }
        [Display(Name = "Descreva aqui")]
        [StringLength(50, MinimumLength = 6)]
        public string RestricaoAlimentar { get; set; }
        [Display(Name = "Descreva aqui")]
        [StringLength(50, MinimumLength = 6)]
        public string RestricaoMedicamentos { get; set; }
        [Display(Name = "Descreva aqui")]
        [StringLength(50, MinimumLength = 6)]
        public string Doencas { get; set; }
        public string DesaparecidoId { get; set; }
        public virtual Desaparecido Desaparecido { get; set; }
        public string VulneravelId { get; set; }
        public virtual Vulneravel Vulneravel { get; set; }

    }
}