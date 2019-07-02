using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public ReportsController(
            IHostingEnvironment env,
            ApplicationDbContext context)
        {
            _env = env;
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
            [DisplayName("مامور")]
            public string Username { get; set; }
            [DisplayName("رده دستگاه")]
            public Guid DeviceTypeId { get; set; }
            public Report Report { get; set; } = new Report();
            public int ParentReportId { get; set; }
        }

        void GenerateReportCode(ref CreateViewModel model)
        {
            var dp = _context.DevicePlaces.Find(model.Report.DevicePlaceId);
            var ds = _context.DeviceStatus.Find(model.Report.DeviceStatusId);
            var rep= _context.Reporters.Find(model.Report.ReporterId);
            model.Report.Code = dp.Code.ToUpper() + ds.Code.ToUpper() + rep.UserName.ToUpper() + DateTime.Now.ToString("yyMMdd");
        }

        // GET: Reports/CreateTripRepairingReport/[ReportId]
        public IActionResult CreateTripRepairingReport(int id)
        {
            CreateViewModel model = new CreateViewModel();
            model.Report = _context.Reports.Find(id);
            model.ParentReportId = id;
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus
                     .Where(ds => (ds.DeviceTypeId == model.Report.DevicePlace.DeviceTypeId && ds.DeviceStatusType == DeviceStatusType.Repair) ||
                     ds.DeviceStatusType == DeviceStatusType.Unrepairable), "StatusId", "Text");
            return View(model);
        }

        // GET: Reports/CreateTripReport/[WagonTripId]
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
            return View(model);
        }

        // GET: Reports/CreateTripReport/DeviceTypeSelected
        public IActionResult DeviceTypeSelected(CreateViewModel model)
        {
            model.Report.Wagon = _context.Wagons.Find(model.Report.WagonId);
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", model.DeviceTypeId);
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces.Where(dp => dp.DeviceTypeId == model.DeviceTypeId), "DevicePlaceId", "Description");
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus
                .Where(ds => ds.DeviceTypeId == model.DeviceTypeId && ds.DeviceStatusType == DeviceStatusType.Malfunction), "StatusId", "Text");
            return View("CreateTripReport", model);
        }

        // POST: Reports/CreateTripReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTripReport(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Report.ReporterId = _context.Reporters.Where(r => r.UserName == model.Username).FirstOrDefault().ReporterId;
                model.Report.Status = ReportStatus.Waiting;
                GenerateReportCode(ref model);
                _context.Reports.Add(model.Report);
                await _context.SaveChangesAsync();
                var wt = _context.WagonTrips.Find(model.Report.WagonTripId);
                return RedirectToAction("Details", "TrainTrips", new { id = model.Report.WagonTrip.TrainTripId });
            }
            return RedirectToAction(nameof(CreateTripReport), model.Report.WagonId);
        }
        // POST: Reports/CreateTripRepairingReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTripRepairingReport(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Report.ReporterId = _context.Reporters.Where(r => r.UserName == model.Username).FirstOrDefault().ReporterId;
                model.Report.ParentReportId = model.ParentReportId;
                model.Report.Status = ReportStatus.Processed;
                var ds = _context.DeviceStatus.Find(model.Report.DeviceStatusId);
                Report ParentReport = _context.Reports.Find(model.ParentReportId);
                if (ds.DeviceStatusType == DeviceStatusType.Repair)
                    ParentReport.Status = ReportStatus.Processed;
                else if (ds.DeviceStatusType == DeviceStatusType.Unrepairable)
                    ParentReport.Status = ReportStatus.Postponed;
                _context.Update(ParentReport);
                GenerateReportCode(ref model);
                _context.Reports.Add(model.Report);
                await _context.SaveChangesAsync();
                var wt = _context.WagonTrips.Find(model.Report.WagonTripId);
                return RedirectToAction("Details", "TrainTrips", new { id = wt.TrainTripId });
            }
            return RedirectToAction(nameof(CreateTripRepairingReport), model.ParentReportId);
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
            ViewData["ParentReportId"] = new SelectList(_context.Reports, "ReportId", "Code", report.ParentReportId);
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName", report.ReporterId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", report.WagonId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,DateTimeCreated,DateTimeModified,ReporterId,Status,DeviceStatusId,ParentReportId,DevicePlaceId,WagonId")] Report report)
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
            ViewData["ParentReportId"] = new SelectList(_context.Reports, "ReportId", "Code", report.ParentReportId);
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

        public IActionResult Export()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"/Excel/ExportReports.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {

                IList<Report> reports = _context.Reports.ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reports");
                int totalRows = reports.Count();

                worksheet.Cells[0, 0].Value = "Code";
                worksheet.Cells[0, 1].Value = "Date";
                worksheet.Cells[0, 1].Value = "Wagon";
                worksheet.Cells[0, 2].Value = "Reporter";
                worksheet.Cells[0, 3].Value = "Device Code";
                worksheet.Cells[0, 4].Value = "Device";
                worksheet.Cells[0, 5].Value = "Status Code";
                worksheet.Cells[0, 6].Value = "Status";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    //worksheet.Cells[row, 1].Value = reports[i].CustomerId;
                    //worksheet.Cells[row, 2].Value = reports[i].CustomerName;
                    //worksheet.Cells[row, 3].Value = reports[i].CustomerEmail;
                    //worksheet.Cells[row, 4].Value = reports[i].CustomerCountry;
                    //i++;
                }

                package.Save();

            }

            return View();
        }
    }
}
