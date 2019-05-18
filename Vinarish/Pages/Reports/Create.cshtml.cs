using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Reports
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
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName");
            ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "Code");
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Code");
            ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FirstName");
            ViewData["WagonId"] = new SelectList(_context.Wagon, "WagonId", "WagonId");
            ViewData["Now"] = Utilities.GetPErsianDateTimeNow();
            return Page();
        }

        [BindProperty]
        public Report Report { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Report.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}