using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.TipoViviendas
{
    public class DeleteModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DeleteModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        [BindProperty]
        public TipoVivienda TipoVivienda { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TipoVivienda = await _context.TipoViviendas.FirstOrDefaultAsync(m => m.TipoViviendaId == id);

            if (TipoVivienda == null)
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

            TipoVivienda = await _context.TipoViviendas.FindAsync(id);

            if (TipoVivienda != null)
            {
                _context.TipoViviendas.Remove(TipoVivienda);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
