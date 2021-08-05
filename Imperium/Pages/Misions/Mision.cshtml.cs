using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Imperium.Data;
using Imperium.Models;

namespace Imperium.Pages.Misions
{
    public class MisionModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public MisionModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public IList<Mision> Mision { get;set; }

        public async Task OnGetAsync()
        {
            Mision = await _context.Misions.ToListAsync();
        }
    }
}
