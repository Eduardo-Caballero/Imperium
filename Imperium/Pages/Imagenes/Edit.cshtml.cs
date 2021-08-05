using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Imperium.Pages.Imagenes
{
    public class EditModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public EditModel(Imperium.Data.ContextDB context)
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
           ViewData["ViviendaId"] = new SelectList(_context.Viviendas, "ViviendaId", "Nombre");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile archivo, int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Imagen = await _context.Imagens
                .Include(i => i.Vivienda).FirstOrDefaultAsync(m => m.ImagenId == id);

            if (archivo!=null)
            {
                BorrarImagen("img", Imagen.Archivo);
                Imagen.Archivo = subirImagen("img", archivo);
            }

            _context.Attach(Imagen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenExists(Imagen.ImagenId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ImagenExists(int id)
        {
            return _context.Imagens.Any(e => e.ImagenId == id);
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

        private string subirImagen(string rutaCarpeta, IFormFile archivoASubir)
        {
            string Carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaCarpeta);

            string NombreUnicoArchivo = Guid.NewGuid().ToString() + " " + archivoASubir.FileName;

            string RutaArchivoDeNombreUnico = Path.Combine(Carpeta, NombreUnicoArchivo);

            using (var InfoArchivoACrear = new FileStream(RutaArchivoDeNombreUnico, FileMode.Create))
            {
                archivoASubir.CopyTo(InfoArchivoACrear);
            }

            return NombreUnicoArchivo;

        }
    }
}
