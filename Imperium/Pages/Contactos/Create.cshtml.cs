using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Contactos
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
            return Page();
        }

        [BindProperty]
        public Contacto Contacto { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contactos.Add(Contacto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Create");
        }
    }
}
