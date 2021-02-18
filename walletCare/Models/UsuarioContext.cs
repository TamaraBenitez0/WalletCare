using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace generico.Models
{


    public class UsuarioContext : DbContext {

        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) {



        }
        

            public DbSet<Ingreso> Ingresos {get;set;}

            public DbSet<Usuario> Usuarios {get;set;}


             public DbSet<Recordatorio> Recordatorios {get;set;}


             public DbSet<Gastos> Gastos {get;set;}

    }


}