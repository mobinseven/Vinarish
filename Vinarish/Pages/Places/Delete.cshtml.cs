using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Places
{
    public class DeleteModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public DeleteModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Place Place { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Place = await _context.Place.FirstOrDefaultAsync(m => m.Id == id);

            if (Place == null)
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

            Place = await _context.Place.FindAsync(id);

            if (Place != null)
            {
                _context.Place.Remove(Place);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
