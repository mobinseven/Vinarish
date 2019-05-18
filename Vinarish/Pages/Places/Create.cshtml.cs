using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Places
{
    public class CreateModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public CreateModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Place Place { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Place.Add(Place);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}