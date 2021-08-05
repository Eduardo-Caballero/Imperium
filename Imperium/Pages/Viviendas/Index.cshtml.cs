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
    public class IndexModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public IndexModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public IList<Vivienda> Vivienda { get;set; }

        public async Task OnGetAsync()
        {
            Vivienda = await _context.Viviendas
                .Include(v => v.Ciudad)
                .Include(v => v.TipoVivienda).ToListAsync();
        }
    }
}
