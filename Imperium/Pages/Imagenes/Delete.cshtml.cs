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

namespace Imperium.Pages.Imagenes
{
    public class DeleteModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DeleteModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        [BindProperty]
        public Imagen Imagen { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Imagen = await _context.Imagens
                .Include(i => i.Vivienda).FirstOrDefaultAsync(m => m.ImagenId == id);

            if (Imagen == null)
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

            Imagen = await _context.Imagens.FindAsync(id);

            if (Imagen != null)
            {
                BorrarImagen("img", Imagen.Archivo);
                _context.Imagens.Remove(Imagen);
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
