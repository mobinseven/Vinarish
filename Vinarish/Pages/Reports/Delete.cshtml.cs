using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.Reports
{
    public class DeleteModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public DeleteModel(Vinarish.Data.TCMMSContext context)
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
                .Include(r => r.AppendixReport)
                .Include(r => r.Cat)
                .Include(r => r.Code)
                .Include(r => r.Place)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon).FirstOrDefaultAsync(m => m.Id == id);

            if (Report == null)
            {
                return NotFound();
            }
            //if(Report.AppendixReports.Count>0) //
            //{
            //    // TODO: message for deleting report with appendix reports.
            //}
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = await _context.Report.FindAsync(id);

            if (Report != null)
            {
                _context.Report.Remove(Report);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
