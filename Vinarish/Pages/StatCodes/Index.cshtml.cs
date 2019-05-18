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
    public class IndexModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public IndexModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        public IList<StatCode> StatCode { get;set; }

        public async Task OnGetAsync()
        {
            StatCode = await _context.StatCode
                .Include(s => s.Cat).ToListAsync();
        }
    }
}
