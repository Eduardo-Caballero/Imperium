using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Imperium.Data;
using Imperium.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Imperium.Pages.Usuarios
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
        ViewData["PerfilId"] = new SelectList(_context.Perfils, "PerfilId", "Nombre");
            return Page();
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile archivo)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Usuario.Fotografia= subirImagen("img", archivo);

            _context.Usuarios.Add(Usuario);
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
