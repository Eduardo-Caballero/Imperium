using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Viviendas
{
    public class DetailsModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DetailsModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
