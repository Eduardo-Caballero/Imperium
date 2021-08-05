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

namespace Imperium.Pages.Viviendas
{
    public class EditModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public EditModel(Imperium.Data.ContextDB context)
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
           ViewData["CiudadId"] = new SelectList(_context.Ciudads, "CiudadId", "Nombre");
           ViewData["TipoViviendaId"] = new SelectList(_context.TipoViviendas, "TipoViviendaId", "Nombre");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile archivo1, IFormFile archivo2, IFormFile archivo3, IFormFile archivo4, int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Vivienda = await _context.Viviendas
                .Include(v => v.Ciudad)
                .Include(v => v.TipoVivienda).FirstOrDefaultAsync(m => m.ViviendaId == id);

            if (archivo1 != null)
            {
                BorrarImagen("img", Vivienda.Imagen1);
                Vivienda.Imagen1 = subirImagen("img", archivo1);
            }
            else
            {
                if (archivo2 != null)
                {
                    BorrarImagen("img", Vivienda.Imagen2);
                    Vivienda.Imagen2 = subirImagen("img", archivo2);
                }
                else
                {
                    if (archivo3 != null)
                    {
                        BorrarImagen("img", Vivienda.Imagen3);
                        Vivienda.Imagen1 = subirImagen("img", archivo3);
                    }
                    else
                    {
                        if (archivo4 != null)
                        {
                            BorrarImagen("img", Vivienda.Imagen4);
                            Vivienda.Imagen1 = subirImagen("img", archivo4);
                        }
                    }
                }
            }

            _context.Attach(Vivienda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViviendaExists(Vivienda.ViviendaId))
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

        private bool ViviendaExists(int id)
        {
            return _context.Viviendas.Any(e => e.ViviendaId == id);
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
