using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Imperium.Models;

namespace Imperium.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string correo { get; set; }

        [BindProperty]
        public string contrasenia { get; set; }

        public Usuario Usuario { get; set; }
        public void OnGet()
        {
        }

        private readonly Imperium.Data.ContextDB _context;
        public LoginModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }


        public void OnGetCerrar()
        {
            HttpContext.Session.Clear(); //Destruir la sesion para que me vuelva a pedir el login
            Response.Redirect("Login"); //Redirecciono al OnGet o principal
        }
        public void OnPostLogin()
        {
            Usuario = _context.Usuarios
            .Where(p => p.Correo == correo && p.Password == contrasenia).FirstOrDefault<Usuario>();



            //Aqui pondias los datos de tu base de datos
            if (Usuario != null)
            {
                //Crear una variable de sesión que llame el id del usuario
                HttpContext.Session.SetString("usuario", Usuario.UsuarioId.ToString());


                //Crear una variable de sesion que se llame usuario
                HttpContext.Session.SetString("correo", Usuario.Correo);



                //Crear una variable de sesion que se llame Contraseña
                HttpContext.Session.SetString("contrasenia", contrasenia);



                //Crear una variable de sesion que se llame Admin
                HttpContext.Session.SetString("Nivel", Usuario.PerfilId.ToString());

               
                    //Redireccionar a una pagina en especifico
                Response.Redirect("Index");
                               
            }
            else
            {
                Response.Redirect("Login");
            }




        }
    }
}
