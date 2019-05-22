using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Models;

namespace VinarishMvc.Views
{
    public class ReportsController : Controller
    {
        private readonly VinarishContext _context;

        public ReportsController(VinarishContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var vinarishContext = _context.Report.Include(r => r.AppendixReport).Include(r => r.Cat).Include(r => r.Code).Include(r => r.Place).Include(r => r.Reporter).Include(r => r.Wagon);
            return View(await vinarishContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.AppendixReport)
                .Include(r => r.Cat)
                .Include(r => r.Code)
                .Include(r => r.Place)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {

            List<SelectListItem> items = new SelectList(_context.Report, "Id", "Id").ToList<SelectListItem>();
            items.Insert(0, new SelectListItem("هیچکدام", ""));
            ViewData["AppendixReportId"] = new SelectList(items, "Value", "Text");
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName");
            ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "FullName");
            ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FullName");
            ViewData["WagonId"] = new SelectList(_context.Wagon, "Id", "WagonId");
            return View();
        }


        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WagonId,PlaceId,CatId,CodeId,DateTime,ReporterId,AppendixReportId,IsValid")] Report report)
        {
            if (ModelState.IsValid)
            {
                report.DateTime = DateTime.Now;
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppendixReportId"] = new SelectList(_context.Report, "Id", "Id", report.AppendixReportId);
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName", report.CatId);
            ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "FullName", report.CodeId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "FullName", report.PlaceId);
            ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FullName", report.ReporterId);
            ViewData["WagonId"] = new SelectList(_context.Wagon, "Id", "WagonId", report.WagonId);
            return View(report);
        }

        // GET: Reports/Append/5
        public async Task<IActionResult> Append(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Main report is the report which we are appending a new one to.
            var MainReport = await _context.Report
                .Include(r => r.Cat)
                .Include(r => r.Code)
                .Include(r => r.Place)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["AppendixReportId"] = new SelectList(new[] { MainReport }, "Id", "Id");//Obviously
            ViewData["CatId"] = new SelectList(new[] { MainReport.Cat }, "Id", "CategoryName");//Some properties are same in both main and appendix
            ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "FullName");//Some aren't
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "FullName");
            ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FullName");
            ViewData["WagonId"] = new SelectList(new[] { MainReport.Wagon }, "Id", "WagonId");
            return View();
        }
        // POST: Reports/Append
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Append([Bind("WagonId,PlaceId,CatId,CodeId,DateTime,ReporterId,AppendixReportId,IsValid")] Report report)
        {
            if (ModelState.IsValid)
            {
                report.DateTime = DateTime.Now;
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var MainReport = await _context.Report
                .Include(r => r.Cat)
                .Include(r => r.Code)
                .Include(r => r.Place)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon)
                .FirstOrDefaultAsync(m => m.Id == report.AppendixReportId);
            ViewData["AppendixReportId"] = new SelectList(new[] { MainReport }, "Id", "Id");//Obviously
            ViewData["CatId"] = new SelectList(new[] { MainReport.Cat }, "Id", "CategoryName");//Some properties are same in both main and appendix
            ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "FullName");//Some aren't
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "FullName");
            ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FullName");
            ViewData["WagonId"] = new SelectList(new[] { MainReport.Wagon }, "Id", "WagonId");
            return View(report);
        }
        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["AppendixReportId"] = new SelectList(_context.Report, "Id", "Id", report.AppendixReportId);
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName", report.CatId);
            ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "FullName", report.CodeId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "FullName", report.PlaceId);
            ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FullName", report.ReporterId);
            ViewData["WagonId"] = new SelectList(_context.Wagon, "Id", "WagonId", report.WagonId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WagonId,PlaceId,CatId,CodeId,DateTime,ReporterId,AppendixReportId,IsValid")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppendixReportId"] = new SelectList(_context.Report, "Id", "Id", report.AppendixReportId);
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName", report.CatId);
            ViewData["CodeId"] = new SelectList(_context.StatCode, "Id", "FullName", report.CodeId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "FullName", report.PlaceId);
            ViewData["ReporterId"] = new SelectList(_context.Person, "Id", "FullName", report.ReporterId);
            ViewData["WagonId"] = new SelectList(_context.Wagon, "Id", "WagonId", report.WagonId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.AppendixReport)
                .Include(r => r.Cat)
                .Include(r => r.Code)
                .Include(r => r.Place)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Report.FindAsync(id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
