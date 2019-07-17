using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinarishMvc.Data;
using VinarishMvc.Models;

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
            List<Report> items = await _context.Reports
                .Include(r => r.AppendixReports)
                .ThenInclude(ar => ar.DeviceStatus)
                .ToListAsync();
            IEnumerable<ReportStatus> status = items.Select(r => r.Status);
            int waiting = status.Where(r => r == ReportStatus.Waiting).Count();

            int postponed = status.Where(r => r == ReportStatus.Postponed).Count();

            int processed = status.Where(r => r == ReportStatus.Processed).Count();
            return View(status);
        }
    }
}