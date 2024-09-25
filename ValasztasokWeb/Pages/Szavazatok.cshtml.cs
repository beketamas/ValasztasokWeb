using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Jelolt Jelolt { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JeloltekListaja.Add(Jelolt);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
