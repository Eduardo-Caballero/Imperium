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
    public class DetailsModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DetailsModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

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
    }
}
