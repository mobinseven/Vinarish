using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Trains
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
        ViewData["HeadId"] = new SelectList(_context.Person, "Id", "FirstName");
        ViewData["OfficerId"] = new SelectList(_context.Person, "Id", "FirstName");
            return Page();
        }

        [BindProperty]
        public Train Train { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Train.Add(Train);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}