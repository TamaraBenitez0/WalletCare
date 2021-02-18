using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace generico.Models{ 


    public class Recordatorio {


        
        [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {get;set;}


         [Required]
        public string Titulo{get;set;}


         [Required]
        public string Texto {get;set;}

        public DateTime FechaCreacion{get;set;}


         [Required]
        public DateTime FechaEnvio {get;set;}


        public bool fueEnviado {get;set;}
         


          
       [Required]
        public string mailUsuario {get;set;}

    }


}