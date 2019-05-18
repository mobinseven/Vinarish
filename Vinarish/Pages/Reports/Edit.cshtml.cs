using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Reports
{
    public class EditModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public EditModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Report Report { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = await _context.Report
                .Include(r => r.Cat)
                .Include(r => r.Code)
                .Include(r => r.Place)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon).FirstOrDefaultAsync(m => m.Id == id);

            if (Report == null)
            {
                return NotFound();
            }
           ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName");
           ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "Code");
           ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Code");
           ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FirstName");
           ViewData["WagonId"] = new SelectList(_context.Wagon, "WagonId", "WagonId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(Report.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
