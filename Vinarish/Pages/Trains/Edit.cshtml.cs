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

namespace Vinarish.Pages.Trains
{
    public class EditModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public EditModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Train Train { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Train = await _context.Train
                .Include(t => t.Head)
                .Include(t => t.Officer).FirstOrDefaultAsync(m => m.TrainId == id);

            if (Train == null)
            {
                return NotFound();
            }
           ViewData["HeadId"] = new SelectList(_context.Person, "Id", "FirstName");
           ViewData["OfficerId"] = new SelectList(_context.Person, "Id", "FirstName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Train).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(Train.TrainId))
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

        private bool TrainExists(int id)
        {
            return _context.Train.Any(e => e.TrainId == id);
        }
    }
}
