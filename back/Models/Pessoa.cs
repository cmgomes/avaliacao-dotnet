using System;
using System.ComponentModel.DataAnnotations;

namespace Neppo.Models
{
    public class Pessoa
    {
        public int id {get; set;}

        [StringLength(50)]
        [Required]
        public string nome {get; set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? dataNascimento {get; set;}

        [StringLength(20)]
        [Required]
        public string documento {get;  set;}

        [StringLength(1)]
        [Required]
        public string sexo {get; set;}

        [StringLength(150)]
        [Required]
        public string endereco {get; set;}
    }
}
