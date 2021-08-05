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

namespace Imperium.Pages.Comentarios
{
    public class EditModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public EditModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        [BindProperty]
        public Comentario Comentario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comentario = await _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Vivienda).FirstOrDefaultAsync(m => m.ComentarioId == id);

            if (Comentario == null)
            {
                return NotFound();
            }
           ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre");
           ViewData["ViviendaId"] = new SelectList(_context.Viviendas, "ViviendaId", "Nombre");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(Comentario.ComentarioId))
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

        private bool ComentarioExists(int id)
        {
            return _context.Comentario.Any(e => e.ComentarioId == id);
        }
    }
}
