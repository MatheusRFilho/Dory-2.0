using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class InitialRegisterDesaparecido
    {
        [Required]
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Sexo { get; set; }

        public string Etinia { get; set; }

        public string CorOlhos { get; set; }

        public string CorCabelo { get; set; }

        public string TipoSanguineo { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        public DateTime DataNascimento { get; set; }

        public string Altura { get; set; }

        public string Peso { get; set; }

        public string Descricao { get; set; }

    }

    public class FinalRegisterDesaparecido
    {
        public string deficienciaMentalRadio { get; set; }

        public string deficienciaMentalText { get; set; }

        public string deficienciaFisicaRadio { get; set; }

        public string deficienciaFisicaText { get; set; }

        public string restricaoAlimentarRadio { get; set; }

        public string restricaoAlimentarText { get; set; }

        public string doencaRadio { get; set; }

        public string doencaText { get; set; }

    }
}