using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Usuarios
{
    public class DeleteModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DeleteModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario = await _context.Usuarios
                .Include(u => u.Ciudad)
                .Include(u => u.Perfil).FirstOrDefaultAsync(m => m.Correo == id);

            if (Usuario == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario = await _context.Usuarios.FindAsync(id);

            if (Usuario != null)
            {
                _context.Usuarios.Remove(Usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
