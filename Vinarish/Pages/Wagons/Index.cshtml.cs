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
    public class IndexModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public IndexModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        public IList<Wagon> Wagon { get;set; }

        public async Task OnGetAsync()
        {
            Wagon = await _context.Wagon
                .Include(w => w.Train).ToListAsync();
        }
    }
}
