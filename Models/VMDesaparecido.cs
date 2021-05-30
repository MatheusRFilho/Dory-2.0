using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Sexos Sexo { get; set; }
        public enum Sexos
        {
            Masculino = 1,
            Feminino = 2,
            Outro = 3,
        }

        public Etinias Cutis { get; set; }
        public enum Etinias
        {
            Amarela = 1,
            Branca = 2,
            Indígena = 3,
            Negra = 4,
            Parda = 5,
        }

        public string CorOlhos { get; set; }

        public string CorCabelo { get; set; }

        public TipoSanguineos TipoSanguineo { get; set; }
        public enum TipoSanguineos
        {
            [
                Description("A+")
            ]
            APositivo,
            [
                Description("A-")
            ]
            ANegativo,
            [
                Description("B+")
            ]
            BPositivo,
            [
                Description("B-")
            ]
            BNegativo,
            [
                Description("O+")
            ]
            OPositivo,
            [
                Description("O-")
            ]
            ONegativo,
            [
                Description("AB+")
            ]
            ABPositivo,
            [
                Description("AB-")
            ]
            ABNegativo,
        }

        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        public string Altura { get; set; }

        public string Peso { get; set; }

        public string Descricao { get; set; }

    }

    public class FinalRegisterDesaparecido
    {
        public int codigo { get; set; }

        public string deficienciaMentalRadio { get; set; }

        public string deficienciaMentalText { get; set; }

        public string deficienciaFisicaRadio { get; set; }

        public string deficienciaFisicaText { get; set; }

        public string restricaoAlimentarRadio { get; set; }

        public string restricaoAlimentarText { get; set; }

        public string restricaoMedicamentosRadio { get; set; }

        public string restricaoMedicamentosText { get; set; }

        public string doencaRadio { get; set; }

        public string doencaText { get; set; }

    }

    public class ConfirmationRegisterDesaparecido
    {
        public int codigo { get; set; }

        public string SeuCPF { get; set; }

        public string CpfDesaparecido { get; set; }

        public string SeuRG { get; set; }

        public string RgDesaparecido { get; set; }

        public string NumeroDoBO { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataBO { get; set; }

    }

    public class EditarInformacoesPessoais
    {
        public int Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Rg { get; set; }

        public Sexos Sexo { get; set; }
        public enum Sexos
        {
            Masculino = 1,
            Feminino = 2,
            Outro = 3,
        }

        public Etinias Cutis { get; set; }
        public enum Etinias
        {
            Amarela = 1,
            Branca = 2,
            Indígena = 3,
            Negra = 4,
            Parda = 5,
        }

        public string CorOlhos { get; set; }

        public string CorCabelo { get; set; }

        public TipoSanguineos TipoSanguineo { get; set; }
        public enum TipoSanguineos
        {
            [
                Description("A+")
            ]
            APositivo,
            [
                Description("A-")
            ]
            ANegativo,
            [
                Description("B+")
            ]
            BPositivo,
            [
                Description("B-")
            ]
            BNegativo,
            [
                Description("O+")
            ]
            OPositivo,
            [
                Description("O-")
            ]
            ONegativo,
            [
                Description("AB+")
            ]
            ABPositivo,
            [
                Description("AB-")
            ]
            ABNegativo,
        }

        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        public string Altura { get; set; }

        public string Peso { get; set; }

        public string Descricao { get; set; }
    }
}