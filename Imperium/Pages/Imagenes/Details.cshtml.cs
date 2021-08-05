using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Imagenes
{
    public class DetailsModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public DetailsModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public Imagen Imagen { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Imagen = await _context.Imagens
                .Include(i => i.Vivienda).FirstOrDefaultAsync(m => m.ImagenId == id);

            if (Imagen == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
