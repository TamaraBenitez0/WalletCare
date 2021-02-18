using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using generico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using walletCare.Models;

namespace walletCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UsuarioContext db;

        

    

        public HomeController(ILogger<HomeController> logger,UsuarioContext contexto)
        {
            _logger = logger;
            db=contexto;
          
        }

        public IActionResult Index()
        {
            return View();
        }

       public IActionResult Registrarse()
        {
            return View();
        }

     


        [HttpPost]
        public IActionResult Registrarse (string email, string nombre,string contraseña) {

            Usuario validarUsuario= db.Usuarios.FirstOrDefault(u => u.Mail==email);

            if(validarUsuario==null){


                  Usuario nuevoUsuario= new Usuario {

                   
                    Mail=email,
                    NombreDeUsuario= nombre,
                    Password= contraseña
                }; 

                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();
                return View("ResultadoRegistro");

            }

               else {

                    ViewBag.MailRegistrado=true;
                   return View("ResultadoRegistro");
               }
           


        }

        public IActionResult ResultadoRegistro () {

            return View();
        }


        public JsonResult AgregarUsuarioALaSession(string email,string contraseña)
        {
            Usuario nuevoUsuario = new Usuario{
               
                    Mail=email,
                    Password= contraseña
            };

            HttpContext.Session.Set<Usuario>("UsuarioLogueado", nuevoUsuario);
            return Json(nuevoUsuario);
        }


            [HttpPost]
            public IActionResult Ingresar (string email, string contraseña) {

                Usuario buscarUsuario= db.Usuarios.FirstOrDefault(u => u.Mail == email);

                if (buscarUsuario!=null) {

                    if(buscarUsuario.Password==contraseña) {

                        HttpContext.Session.Set<Usuario>("UsuarioLogueado", buscarUsuario);
                        return RedirectToAction("Index","User");

                    }
                    else {

                        ViewBag.LoginError = true ;
                        return View ("Ingresar");
                    }


                       } else {

                        ViewBag.LoginError = true ;
                        return View ("Ingresar");
                    
                }

            }


            public JsonResult ConsultarUsuarioEnSesion() {


                Usuario usuario = HttpContext.Session.Get<Usuario>("UsuarioLogueado");
                return Json(usuario);

            }


            public IActionResult Ingresar()
        {
            return View();
        }

       

         public JsonResult ConsultarUsuarios(){


            return Json(db.Usuarios.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
