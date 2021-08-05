using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Comentarios
{
    public class DeleteModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DeleteModel(Imperium.Data.ContextDB context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comentario = await _context.Comentario.FindAsync(id);

            if (Comentario != null)
            {
                _context.Comentario.Remove(Comentario);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
