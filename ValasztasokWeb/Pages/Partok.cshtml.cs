using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValasztasokWeb.Models;

namespace ValasztasokWeb.Pages
{
    public class SzavazatokModel : PageModel
    {
        private readonly ValasztasokWeb.Models.ValasztasDbContext _context;

        public SzavazatokModel(ValasztasokWeb.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        public IList<Part> Part { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Part = await _context.Partok.ToListAsync();
        }
    }
}
