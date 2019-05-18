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

namespace Vinarish.Pages.StatCodes
{
    public class EditModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public EditModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StatCode StatCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StatCode = await _context.StatCode
                .Include(s => s.Cat).FirstOrDefaultAsync(m => m.Id == id);

            if (StatCode == null)
            {
                return NotFound();
            }
           ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StatCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatCodeExists(StatCode.Id))
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

        private bool StatCodeExists(int id)
        {
            return _context.StatCode.Any(e => e.Id == id);
        }
    }
}
