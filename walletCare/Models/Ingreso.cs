using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace generico.Models{


    public class Ingreso {

        [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {get;set;}

        [Required]
        public double Aporte {get;set;}

        [Required]
        public DateTime fecha {get;set;}

        
       [Required]
        public string mailUsuario {get;set;}

    }
    
}