using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reports.Include(r => r.Device).Include(r => r.DeviceStatus).Include(r => r.Report1).Include(r => r.Reporter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Device)
                .Include(r => r.DeviceStatus)
                .Include(r => r.Report1)
                .Include(r => r.Reporter)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["DeviceId"] = new SelectList(_context.Devices, "DeviceId", "Code");
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code");
            ViewData["AppendixReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId");
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,DateTimeCreated,DateTimeModified,DeviceId,ReporterId,Status,DeviceStatusId,AppendixReportId")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceId"] = new SelectList(_context.Devices, "DeviceId", "Code", report.DeviceId);
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code", report.DeviceStatusId);
            ViewData["AppendixReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId", report.AppendixReportId);
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId", report.ReporterId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["DeviceId"] = new SelectList(_context.Devices, "DeviceId", "Code", report.DeviceId);
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code", report.DeviceStatusId);
            ViewData["AppendixReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId", report.AppendixReportId);
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId", report.ReporterId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,DateTimeCreated,DateTimeModified,DeviceId,ReporterId,Status,DeviceStatusId,AppendixReportId")] Report report)
        {
            if (id != report.ReportId)
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
                    if (!ReportExists(report.ReportId))
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
            ViewData["DeviceId"] = new SelectList(_context.Devices, "DeviceId", "Code", report.DeviceId);
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code", report.DeviceStatusId);
            ViewData["AppendixReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId", report.AppendixReportId);
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId", report.ReporterId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Device)
                .Include(r => r.DeviceStatus)
                .Include(r => r.Report1)
                .Include(r => r.Reporter)
                .FirstOrDefaultAsync(m => m.ReportId == id);
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
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}
