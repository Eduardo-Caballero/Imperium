using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Usuarios
{
    public class CrearLoginModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public CrearLoginModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CiudadId"] = new SelectList(_context.Ciudads, "CiudadId", "Nombre");
        ViewData["PerfilId"] = new SelectList(_context.Perfils, "PerfilId", "PerfilId");
            return Page();
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Usuario.PerfilId = 1;
            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
}
