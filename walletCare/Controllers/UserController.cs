using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using generico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using walletCare.Models;

namespace walletCare.Controllers
{


     public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UsuarioContext db;

        public UserController(ILogger<UserController> logger,UsuarioContext usuarioContext)
        {
            _logger = logger;
            db = usuarioContext ;
        }


     public IActionResult Index()

        {

          Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
             ViewBag.UsuarioNombre= usuario.NombreDeUsuario;
        

            return View();
        }

    public IActionResult NuevoAhorro() {

        return View();
    }


    public IActionResult ResultadoRecordatorio() {

        return View();

    }

    public IActionResult ResultadoIngreso() {

        return View();
    }

    public IActionResult ResultadoGastos() {

        return View();
    }

    public IActionResult NuevoRecordatorio() {


        return View();
    }


    public IActionResult AgregarNuevoGasto() {


            return View();
    }


        public IActionResult SacarUsuarioEnSesion() {


             HttpContext.Session.Remove("UsuarioLogueado");
             return RedirectToAction("Index","Home");
        }


         public IActionResult AgregarIngreso(double aporte) {

                Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
               

                if(usuario !=null){


                Usuario usuarioBase = db.Usuarios.FirstOrDefault(u => u.Mail.Equals(usuario.Mail));
                Ingreso nuevoIngreso = new Ingreso(){

                  
                    Aporte=aporte,
                    fecha=DateTime.Now,
                    mailUsuario = usuarioBase.Mail
                    
                };

              

        
                db.Ingresos.Add(nuevoIngreso);
                db.SaveChanges();

                 return RedirectToAction("ResultadoIngreso","User");
                
              }

                else {
                        ViewBag.IngresoError= true;
                    return RedirectToAction("ResultadoIngreso","User");
                }

        } 


        public IActionResult AgregarGasto (double gasto,string nombre,string categoria) {


            Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
               

                if(usuario !=null){ 

                    
                Usuario usuarioBase = db.Usuarios.FirstOrDefault(u => u.Mail.Equals(usuario.Mail));
               Gastos nuevoGasto = new Gastos(){

                  
                    Aporte=gasto,
                    Nombre=nombre,
                    fecha=DateTime.Now,
                    Categoria=categoria,
                    mailUsuario = usuarioBase.Mail
                    
                };

                db.Gastos.Add(nuevoGasto);
                db.SaveChanges();

                    return RedirectToAction("ResultadoGastos","User");
                }


                     else {
                        ViewBag.GastoError= true;
                    return RedirectToAction("ResultadoIngreso","User");
                }


        }



        public IActionResult AgregarRecordatorio(string titulo, string texto,int dia,int anio,int mes,int hora,int minutos) {


            Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");

             if(usuario !=null){ 

                 Usuario usuarioBase = db.Usuarios.FirstOrDefault(u => u.Mail.Equals(usuario.Mail));    
                
                DateTime envio = new DateTime(anio,mes,dia,hora,minutos,0); 
               

                Recordatorio nuevoRecodatorio  = new Recordatorio () {


                    

                        Titulo=titulo,
                        Texto= texto,
                        FechaCreacion=DateTime.Now,
                        FechaEnvio= envio , 
                        fueEnviado = false , 
                        mailUsuario = usuarioBase.Mail

                } ;


                    db.Recordatorios.Add(nuevoRecodatorio);
                    db.SaveChanges();

                   return RedirectToAction("ResultadoRecordatorio","User");

             }

              else {
                        ViewBag.RecordatorioError= true;
                    return RedirectToAction("ResultadoRecordatorio","User");
                }


        }



        public JsonResult ConsultarIngresos(){


            return Json(db.Ingresos.ToList());
        }



    public JsonResult ConsultarRecordatorios () {

        return Json(db.Recordatorios.ToList());
    }


    


        public IActionResult MostrarIngresos() {

           Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
           if (usuario != null ) {

               List<Ingreso> ahorros = new List<Ingreso>();
               ahorros = db.Ingresos.Where(i => i.mailUsuario.Equals(usuario.Mail)).ToList();

                 if(ahorros.Count==0){

                    return View("SinIngresos");
                }


                else {


                     return View ("MostrarIngresos",ahorros);

                }
               

           }
            else {

             return Redirect("/User/ErrorUser");
            
            }
        }


        public IActionResult MostrarGastos() {


              Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");

             if (usuario != null ) { 


                    List<Gastos> gastos = new List<Gastos>();
                    gastos= db.Gastos.Where(g => g.mailUsuario.Equals(usuario.Mail)).ToList();
                    if(gastos.Count==0) {

                        return View("SinGastos");

                    }  else {


                     return View ("MostrarGastos",gastos);

                }

             }

             else {

             return Redirect("/User/ErrorUser");
            
            }

        }



    public IActionResult SinGastos() {

        return View();
    }


    public IActionResult SinRecordatorios() {

        return View();
    }

        public IActionResult MostrarRecordatorios() {


             Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
            if (usuario != null ) {

                List <Recordatorio> recordatorios = new List<Recordatorio>();
                recordatorios = db.Recordatorios.Where(r => r.mailUsuario.Equals(usuario.Mail)).ToList();


                  if(recordatorios.Count==0){

                    return View("SinRecordatorios");
                }

                else {

                    return View ("MostrarRecordatorios",recordatorios);
                }

               
           }


            else {

             return Redirect("/User/ErrorUser");
            
            }

        }


        public IActionResult PorMes() {

            return View();
        }


        public int elegirMes(string mes) {

          
        var map = new Dictionary<string, int>();

        
        map.Add("Enero", 1); map.Add("Febrero", 2); map.Add("Marzo", 3);
        map.Add("Abril", 4);  map.Add("Mayo", 5); map.Add("Junio", 6);
         map.Add("Julio", 7);  map.Add("Agosto", 8); map.Add("Septiembre", 9);
          map.Add("Octubre",10);  map.Add("Noviembre", 11);  map.Add("Diciembre", 12);

        
            return map.FirstOrDefault(m => m.Key == mes).Value;

        }


        public IActionResult PorAporte() {

            return View();
        }

        [HttpPost]
        public IActionResult FiltrarPorAporte (double aporte) {


             Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
            if (usuario != null ) { 

                     List<Ingreso> ahorros = new List<Ingreso>();
                ahorros = db.Ingresos.Where (i => i.Aporte.Equals(aporte) && i.mailUsuario.Equals(usuario.Mail)).ToList();

                   if(ahorros.Count==0){

                    return View("NoExisteAporte");
                }


                    else {
                         return View ("MostrarIngresos",ahorros);
                   }

            }  else {

             return Redirect("/User/ErrorUser");
            
            }

        }


    public IActionResult GastosPorAporte() {


        return View();
    }


        [HttpPost]
       public IActionResult VerGastosPorAporte(Double aporte) {


            Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");

             if (usuario != null ) { 


                 List<Gastos> gastos = new List<Gastos>();
                  gastos = db.Gastos.Where (g => g.Aporte.Equals(aporte) && g.mailUsuario.Equals(usuario.Mail)).ToList();

                  if(gastos.Count==0){

                    return View("NoExisteAporte");
                }
                 else {
                         return View ("MostrarGastos",gastos);
                   }
             }

             else {

             return Redirect("/User/ErrorUser");
            
            }

       }


        [HttpPost]
        public IActionResult filtrarPorMes(string mes) {

             Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
            if (usuario != null ) { 

                List<Ingreso> ahorros = new List<Ingreso>();
                ahorros = db.Ingresos.Where (i => i.fecha.Month.Equals(elegirMes(mes)) && i.mailUsuario.Equals(usuario.Mail)).ToList();

                if(ahorros.Count==0){

                    return View("SinIngresos");
                }


                    else {
                         return View ("MostrarIngresos",ahorros);
                   }

           }
            else {

             return Redirect("/User/ErrorUser");
            
            }

              
        }


        public IActionResult filtrarPorMesGastos(string mes) {


              Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
                 if (usuario != null ) { 


                     List<Gastos> gastos = new List <Gastos>();
                     gastos = db.Gastos.Where (g => g.fecha.Month.Equals(elegirMes(mes)) && g.mailUsuario.Equals(usuario.Mail)).ToList();
                    
                    if(gastos.Count==0){

                        return View("SinGastos");

                        
                        }
                     
                     else {
                         return View ("MostrarGastos",gastos);
                   }

                    


                 } else {

             return Redirect("/User/ErrorUser");


        }

        }



        public IActionResult PorMesGastos() {

            return View();
        }

        public IActionResult MostrarRecordatoriosCercanos() {


            Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");

             if (usuario != null ) {


                   List <Recordatorio> recordatorios = new List<Recordatorio>();
                recordatorios = db.Recordatorios.Where(r => r.mailUsuario.Equals(usuario.Mail)).ToList();

                List <Recordatorio> recordatorios2 = new List<Recordatorio>();

                foreach(Recordatorio r in recordatorios) {

                    if(  (DateTime.Now- r.FechaEnvio).Days<=2 &&  (DateTime.Now- r.FechaEnvio).Days>=0  ){


                        recordatorios2.Add(r);
                    }

                }



                if(recordatorios2.Count==0){

                    return View("SinRecordatoriosCercanos");
                }

               
                    else {
                         return View ("MostrarRecordatorios",recordatorios2);
                   }



              }

             else {

             return Redirect("/User/ErrorUser");
            
            }

        }

        






        public IActionResult SinRecordatoriosCercanos () {

            return View ();
        }


    public IActionResult ErrorUser() {

        return View();
    }


    public IActionResult EliminarIngreso(int ID) {

        Ingreso ingreso = db.Ingresos.FirstOrDefault(i => i.ID == ID);
        
        db.Ingresos.Remove(ingreso);
        db.SaveChanges();

        return Redirect("MostrarIngresos");

    }

    public IActionResult EliminarGasto(int ID) {

        Gastos gasto= db.Gastos.FirstOrDefault(g => g.ID == ID);

        db.Gastos.Remove(gasto);
        db.SaveChanges();

        return Redirect("MostrarGastos");


    }


    public IActionResult EliminarRecordatorio(int ID)  {


        Recordatorio recordatorio = db.Recordatorios.FirstOrDefault(r => r.ID == ID);

        db.Recordatorios.Remove(recordatorio);
        db.SaveChanges();

        return Redirect ("MostrarRecordatorios");

    }



        [HttpPost]
       public IActionResult EnviarAlerta(string Titulo,string Texto,int ID) {


            Usuario usuario= HttpContext.Session.Get<Usuario>("UsuarioLogueado");
            Recordatorio recordatorio = db.Recordatorios.FirstOrDefault(r => r.ID == ID);

            if (usuario != null ) { 

                 try
                {
                    MailMessage correo= new MailMessage();
                    correo.From=new MailAddress("grupo1comit@gmail.com");
                    correo.To.Add(usuario.Mail);
                    correo.Subject= Titulo;
                    correo.Body= Texto;
                    correo.IsBodyHtml= true;
                    correo.Priority= MailPriority.Normal;
                    SmtpClient smtp= new SmtpClient();
                    smtp.Host= "smtp.gmail.com";
                    smtp.Port=25;
                    smtp.EnableSsl=true;
                    smtp.UseDefaultCredentials=true;
                    string scuentaCorreo="grupo1comit@gmail.com";
                    string sPasswordCorreo="grupo1com";
                    smtp.Credentials= new System.Net.NetworkCredential(scuentaCorreo,sPasswordCorreo);
                    smtp.Send(correo);

                    recordatorio.fueEnviado=true;
                }
                catch (Exception ex)
                {
                    
                    ViewBag.Error= ex.Message;
                }

                 return Redirect ("CorreoEnviado");

            }


            else {

                     return Redirect("/User/ErrorUser");

            }



       }


        public IActionResult CorreoEnviado() {

            return View();
        }



    }
}