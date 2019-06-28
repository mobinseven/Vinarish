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
            var applicationDbContext = _context.Reports.Include(r => r.DevicePlace).Include(r => r.DeviceStatus).Include(r => r.ParentReport).Include(r => r.Reporter).Include(r => r.Wagon);
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
                .Include(r => r.DevicePlace)
                .Include(r => r.DeviceStatus)
                .Include(r => r.ParentReport)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        public class CreateViewModel
        {
            public string Username { get; set; }
            public Guid DeviceTypeId { get; set; }
            public Report Report { get; set; } = new Report();
        }

        // GET: Reports/Create/[WagonTripId]
        public IActionResult CreateTripReport(Guid id)
        {
            var wagonTrip = _context.WagonTrips.Find(id);
            var wagon = wagonTrip.Wagon;
            CreateViewModel model = new CreateViewModel();
            model.Report.WagonId = wagon.WagonId;
            model.Report.Wagon = wagon;
            model.Report.WagonTripId = wagonTrip.WagonTripId;
            model.Report.WagonTrip = wagonTrip;

            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name");
            return View("Create", model);
        }

        // GET: Reports/Create/DeviceTypeSelected
        public IActionResult DeviceTypeSelected(CreateViewModel model)
        {
            model.Report.Wagon = _context.Wagons.Find(model.Report.WagonId);
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", model.DeviceTypeId);
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces.Where(dp => dp.DeviceTypeId == model.DeviceTypeId), "DevicePlaceId", "Description");
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus.Where(ds => ds.DeviceTypeId == model.DeviceTypeId), "StatusId", "Text");
            return View("Create", model);
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Report.ReporterId = _context.Reporters.Where(r => r.UserName == model.Username).FirstOrDefault().ReporterId;
                model.Report.Status = ReportStatus.Waiting;
                _context.Add(model.Report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(CreateTripReport), model.Report.WagonId);
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
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code", report.DevicePlaceId);
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code", report.DeviceStatusId);
            ViewData["AppendixReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId", report.AppendixReportId);
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName", report.ReporterId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", report.WagonId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,DateTimeCreated,DateTimeModified,ReporterId,Status,DeviceStatusId,AppendixReportId,DevicePlaceId,WagonId")] Report report)
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
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code", report.DevicePlaceId);
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code", report.DeviceStatusId);
            ViewData["AppendixReportId"] = new SelectList(_context.Reports, "ReportId", "ReportId", report.AppendixReportId);
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName", report.ReporterId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", report.WagonId);
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
                .Include(r => r.DevicePlace)
                .Include(r => r.DeviceStatus)
                .Include(r => r.ParentReport)
                .Include(r => r.Reporter)
                .Include(r => r.Wagon)
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
