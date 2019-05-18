using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Categories
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
        ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}