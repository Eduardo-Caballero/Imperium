﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly Imperium.Data.ContextDB _context;

        public IndexModel(Imperium.Data.ContextDB context)
        {
            _context = context;
        }

        public IList<TipoVivienda> TipoVivienda { get;set; }

        public async Task OnGetAsync()
        {
            TipoVivienda = await _context.TipoViviendas.ToListAsync();
        }
    }
}
