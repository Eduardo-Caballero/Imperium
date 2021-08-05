using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;
using Microsoft.AspNetCore.Http;

namespace Imperium.Pages.Viviendas
{
    public class VistaModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public VistaModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public Vivienda Vivienda { get; set; }

        public IList<Comentario> Comentario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            Vivienda = await _context.Viviendas
                .Include(v => v.Ciudad)
                .Include(v => v.TipoVivienda).FirstOrDefaultAsync(m => m.ViviendaId == id);


            var estado = "Aprobado";

            //Esta consulta intenta traer los comentarios de la casa correspondiente y el nombre del usuario que realizó el comentario
            //Falta realizar la impresión de los datos en el html
            //Falta ver como traer el nombre del usuario
            //Debe salir así (Nombre + Apellido Paterno + Apellido Materno)
            //Buscar como traer esos datos y concatenarlos
            Comentario = await _context.Comentario
                .Include(c => c.Usuario)//Inner join a la tabla usuarios
                .Where(c => c.ViviendaId == id  && c.Status==estado).ToListAsync();//Condición de que traiga los datos según la id de la casa que se esta viendo

            //Aún falta hacer que funcione el formulario de contacto
            //Primero hay que acabar lo que falta de los comentarios, después empezamos con el contacto

            HttpContext.Session.SetString("casa", id.ToString());

            if (Vivienda == null)
            {
                return NotFound();
            }

            if (Comentario==null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
