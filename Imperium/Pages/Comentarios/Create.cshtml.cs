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

namespace Imperium.Pages.Comentarios
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
        ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
        ViewData["ViviendaId"] = new SelectList(_context.Viviendas, "ViviendaId", "Nombre");
            return Page();
        }

        [BindProperty]
        public Comentario Comentario { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Comentario.Status = "En aprobación";
            Comentario.ViviendaId = int.Parse(HttpContext.Session.GetString("casa"));
            Comentario.UsuarioId= int.Parse(HttpContext.Session.GetString("usuario"));

            _context.Comentario.Add(Comentario);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Viviendas/Catalogo");
        }
    }
}
