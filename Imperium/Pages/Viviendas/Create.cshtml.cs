using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Imperium.Data;
using Imperium.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Imperium.Pages.Viviendas
{
    public class CreateModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public CreateModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CiudadId"] = new SelectList(_context.Ciudads, "CiudadId", "Nombre");
        ViewData["TipoViviendaId"] = new SelectList(_context.TipoViviendas, "TipoViviendaId", "Nombre");
            return Page();
        }

        [BindProperty]
        public Vivienda Vivienda { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile archivo1, IFormFile archivo2, IFormFile archivo3, IFormFile archivo4)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Vivienda.Imagen1 = subirImagen("img", archivo1);
            Vivienda.Imagen2 = subirImagen("img", archivo2);
            Vivienda.Imagen3 = subirImagen("img", archivo3);
            Vivienda.Imagen4 = subirImagen("img", archivo4);



            _context.Viviendas.Add(Vivienda);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
