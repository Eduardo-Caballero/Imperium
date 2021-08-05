using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Contactos
{
    public class DetailsModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DetailsModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public Contacto Contacto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contacto = await _context.Contactos.FirstOrDefaultAsync(m => m.ContactoId == id);

            if (Contacto == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
