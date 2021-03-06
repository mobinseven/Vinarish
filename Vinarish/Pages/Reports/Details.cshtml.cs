﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public DetailsModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
