using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Wagons
{
    public class DeleteModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public DeleteModel(Vinarish.Data.TCMMSContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Wagon = await _context.Wagon.FindAsync(id);

            if (Wagon != null)
            {
                _context.Wagon.Remove(Wagon);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
