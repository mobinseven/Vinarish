using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.StatCodes
{
    public class DetailsModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public DetailsModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
