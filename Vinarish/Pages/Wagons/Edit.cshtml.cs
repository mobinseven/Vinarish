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

namespace Vinarish.Pages.Wagons
{
    public class EditModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public EditModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Wagon Wagon { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Wagon = await _context.Wagon
                .Include(w => w.Train).FirstOrDefaultAsync(m => m.WagonId == id);

            if (Wagon == null)
            {
                return NotFound();
            }
           ViewData["TrainId"] = new SelectList(_context.Train, "TrainId", "TrainId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Wagon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WagonExists(Wagon.WagonId))
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

        private bool WagonExists(int id)
        {
            return _context.Wagon.Any(e => e.WagonId == id);
        }
    }
}
