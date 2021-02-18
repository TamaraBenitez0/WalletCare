using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace generico.Models{ 


    public class Gastos{


         [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {get;set;}

         [Required]
        public double Aporte{get;set;}

        [Required]
        public string Nombre{get;set;}


         [Required]
        public DateTime fecha {get;set;}

        [Required]
        public string Categoria{get;set;}

        [Required]
        public string mailUsuario {get;set;}

    }


}

