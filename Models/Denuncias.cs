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
        [StringLength(255, MinimumLength = 15)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataDenuncia { get; set; }

        [Required]
        public string TipoDenuncia { get; set; } // perguntar dps pro allbert

        [Required]
        [EnumDataType(typeof (StatusDenuncia))]
        public string Status { get; set; }

        public string RepostaAdm { get; set; }

        // falta chaves estrangeiras

        public enum StatusDenuncia
        {
            Aberto = 0,
            Resolvido = 1
        }
    }
}