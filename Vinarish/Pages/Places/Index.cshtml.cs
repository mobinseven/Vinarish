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
    public class IndexModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public IndexModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        public IList<Place> Place { get;set; }

        public async Task OnGetAsync()
        {
            Place = await _context.Place.ToListAsync();
        }
    }
}
