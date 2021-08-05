using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Ciudades
{
    public class DeleteModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DeleteModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        [BindProperty]
        public Ciudad Ciudad { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ciudad = await _context.Ciudads.FirstOrDefaultAsync(m => m.CiudadId == id);

            if (Ciudad == null)
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

            Ciudad = await _context.Ciudads.FindAsync(id);

            if (Ciudad != null)
            {
                _context.Ciudads.Remove(Ciudad);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
