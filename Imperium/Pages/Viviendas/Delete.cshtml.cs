using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Imperium.Pages.Viviendas
{
    public class DeleteModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DeleteModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        [BindProperty]
        public Vivienda Vivienda { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vivienda = await _context.Viviendas
                .Include(v => v.Ciudad)
                .Include(v => v.TipoVivienda).FirstOrDefaultAsync(m => m.ViviendaId == id);

            if (Vivienda == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vivienda = await _context.Viviendas.FindAsync(id);

            if (Vivienda != null)
            {
                BorrarImagen("img", Vivienda.Imagen1);
                BorrarImagen("img", Vivienda.Imagen2);
                BorrarImagen("img", Vivienda.Imagen3);
                BorrarImagen("img", Vivienda.Imagen4);
                _context.Viviendas.Remove(Vivienda);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public void BorrarImagen(string rutaCarpeta, string NombreArchivoBorrar)
        {
            string Carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaCarpeta);

            string RutaArchivoDeNombreUnico = Path.Combine(Carpeta, NombreArchivoBorrar);

            FileInfo InformacionArchivo = new FileInfo(RutaArchivoDeNombreUnico);

            if (InformacionArchivo.Exists)
            {
                InformacionArchivo.Delete();
            }
        }
    }
}
