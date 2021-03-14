﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dory2.Models
{
    public class Desaparecido
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Data que a pessoa foi encontrada")]
        [DataType(DataType.Date)]
        public DateTime Encontrado{ get; set; }

        [Required]
        public int PessoaId { get; set; }

        public int VulneravelId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual Vulneravel Vulneravel { get; set; }

    }
}