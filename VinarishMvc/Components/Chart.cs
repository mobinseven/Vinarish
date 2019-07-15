using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinarishMvc.Data;

namespace VinarishMvc.ViewComponents
{
    public class ChartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ChartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _context.Reports
                .Include(r => r.AppendixReports)
                .ThenInclude(ar => ar.DeviceStatus)
                .ToListAsync();
            return View(items);
        }
    }
}