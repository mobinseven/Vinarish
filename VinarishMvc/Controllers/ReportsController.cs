using MD.PersianDateTime.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VinarishMvc.Areas.Authentication.Data;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    [Authorize(Roles = RolesList.User.RoleName)]
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
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Report, Wagon> applicationDbContext = _context.Reports.Include(r => r.DevicePlace).Include(r => r.DeviceStatus).Include(r => r.ParentReport).Include(r => r.Reporter).Include(r => r.Wagon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report report = await _context.Reports
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
            [DisplayName(Expressions.Reporter)]
            public string Username { get; set; }

            [DisplayName(Expressions.DeviceTypes)]
            public Guid DeviceTypeId { get; set; }

            public Report Report { get; set; } = new Report();

            public int ParentReportId { get; set; }

            [DisplayName(Expressions.Assistants)]
            public Dictionary<string, bool> Assistants { get; set; }
        }

        private void GenerateReportCode(ref CreateViewModel model)
        {
            Report report = model.Report;
            GenerateReportCode(ref report);
            model.Report = report;
        }

        private void GenerateReportCode(ref Report report)
        {
            report.Code = GenerateReportCode(report.DevicePlaceId, report.DeviceStatusId, report.ReporterId, report.WagonId, DateTime.Now);
        }

        private string GenerateReportCode(Guid DevicePlaceId, Guid DeviceStatusId, int ReporterId, Guid WagonId, DateTime date)
        {
            DevicePlace dp = _context.DevicePlaces.Find(DevicePlaceId);
            DeviceStatus ds = _context.DeviceStatus.Find(DeviceStatusId);
            Reporter rep = _context.Reporters.Find(ReporterId);
            Wagon w = _context.Wagons.Find(WagonId);
            return w.Number + dp.Code.ToUpper() + ds.Code.ToUpper() + rep.UserName.ToUpper() + date.ToString("yyMMdd");
        }

        // GET: Reports/CreateTripReport/[WagonTripId]
        public async Task<IActionResult> CreateTripReport(Guid id)
        {
            WagonTrip wagonTrip = await _context.WagonTrips.Include(wt => wt.Wagon).Where(wt => wt.WagonTripId == id).FirstOrDefaultAsync();
            Wagon wagon = wagonTrip.Wagon;
            CreateViewModel model = new CreateViewModel();
            model.Report.WagonId = wagon.WagonId;
            model.Report.Wagon = wagon;
            model.Report.WagonTripId = wagonTrip.WagonTripId;
            model.Report.WagonTrip = wagonTrip;

            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name");
            return View("Create", model);
        }

        // GET: Reports/CreateWagonReport/[WagonId]
        public IActionResult CreateWagonReport(Guid id)
        {
            Wagon wagon = _context.Wagons.Find(id);
            CreateViewModel model = new CreateViewModel();
            model.Report.WagonId = wagon.WagonId;
            model.Report.Wagon = wagon;

            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name");
            return View("Create", model);
        }

        // GET: Reports/CreateTripReport/DeviceTypeSelected
        public IActionResult DeviceTypeSelected(CreateViewModel model)
        {
            model.Report.Wagon = _context.Wagons.Find(model.Report.WagonId);
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", model.DeviceTypeId);
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces.Where(dp => dp.DeviceTypeId == model.DeviceTypeId), "DevicePlaceId", "Description");
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus
                .Where(ds => ds.DeviceTypeId == model.DeviceTypeId && ds.DeviceStatusType == DeviceStatusType.Malfunction), "StatusId", "Text");
            return View("Create", model);
        }

        // POST: Reports/CreateTripReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReport(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Report.ReporterId = _context.Reporters.Where(r => r.UserName == model.Username).FirstOrDefault().ReporterId;
                GenerateReportCode(ref model);
                _context.Reports.Add(model.Report);
                await _context.SaveChangesAsync();
                if (model.Report.WagonTripId != null)
                {
                    WagonTrip wt = _context.WagonTrips.Find(model.Report.WagonTripId);
                    return RedirectToAction("Details", "TrainTrips", new { id = model.Report.WagonTrip.TrainTripId });
                }
                else
                {
                    return RedirectToAction("Details", "Wagons", new { id = model.Report.Wagon.WagonId });
                }
            }
            return RedirectToAction(nameof(CreateReport), model.Report.WagonId);
        }

        // GET: Reports/Edit/[ReportId]
        public IActionResult Edit(int id)
        {
            CreateViewModel model = new CreateViewModel
            {
                Report = _context.Reports
                .Include(r => r.DevicePlace)
                .Include(r => r.Wagon)
                .Where(r => r.ReportId == id)
                .FirstOrDefault(),
                ParentReportId = id
            };
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus
                      .Where(ds => (ds.DeviceTypeId == model.Report.DevicePlace.DeviceTypeId && ds.DeviceStatusType == DeviceStatusType.Malfunction)), "StatusId", "Text");
            return View("CreateChildReport", model);
        }

        // GET: Reports/CreateRepairingReport/[ReportId]
        public IActionResult CreateRepairingReport(int id)
        {
            if (!ReportExists(id))
                return NotFound();
            CreateViewModel model = new CreateViewModel
            {
                Report = _context.Reports
                .Include(r => r.DevicePlace)
                .Include(r => r.Wagon)
                .Where(r => r.ReportId == id)
                .FirstOrDefault(),
                ParentReportId = id,
                Assistants = new Dictionary<string, bool>()
            };
            ViewData["SiteId"] = new SelectList(_context.Sites, "SiteId", "Name");
            List<Reporter> assistants = _context.Reporters.OrderBy(r => r.UserName).ToList();
            foreach (Reporter assitant in assistants)
            {
                model.Assistants.Add(assitant.UserName, false);
            }
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus
                      .Where(ds => (ds.DeviceTypeId == model.Report.DevicePlace.DeviceTypeId && ds.DeviceStatusType == DeviceStatusType.Repair)), "StatusId", "Text");
            return View("CreateChildReport", model);
        }

        // GET: Reports/CreateUnrepairedReport/[ReportId]
        public IActionResult CreateUnrepairedReport(int id)
        {
            CreateViewModel model = new CreateViewModel
            {
                Report = _context.Reports
                .Include(r => r.DevicePlace)
                .Include(r => r.Wagon)
                .Where(r => r.ReportId == id)
                .FirstOrDefault(),
                ParentReportId = id
            };
            ViewData["SiteId"] = new SelectList(_context.Sites, "SiteId", "Name");
            ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus
                      .Where(ds => ds.DeviceStatusType == DeviceStatusType.Unrepairable), "StatusId", "Text");
            return View("CreateChildReport", model);
        }

        // POST: Reports/CreateRepairingReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChildReport(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Report NewReport = model.Report;
                NewReport.ReporterId = _context.Reporters.Where(r => r.UserName == model.Username).FirstOrDefault().ReporterId;
                NewReport.ParentReportId = model.ParentReportId;
                GenerateReportCode(ref NewReport);
                _context.Reports.Add(NewReport);
                await _context.SaveChangesAsync();
                if (model.Assistants != null)
                {
                    List<Assistant> assistants = new List<Assistant>();
                    foreach (var item in model.Assistants)
                    {
                        if (item.Value)
                        {
                            assistants.Add(new Assistant
                            {
                                ReportId = NewReport.ReportId,
                                PersonId = _context.Reporters.Where(r => r.UserName == item.Key).FirstOrDefault().ReporterId
                            });
                        }
                    }
                    _context.AddRange(assistants);
                    await _context.SaveChangesAsync();
                }
                if (NewReport.WagonTripId != null)
                {
                    WagonTrip wt = _context.WagonTrips.Find(NewReport.WagonTripId);
                    return RedirectToAction(nameof(Details), "TrainTrips", new { id = wt.TrainTripId });
                }
                else
                {
                    return RedirectToAction("Details", "Wagons", new { id = NewReport.WagonId });
                }
            }
            return View("CreateChildReport", model);
        }

        //// GET: Reports/Edit/5
        //[Authorize(Roles = RolesList.Admin.RoleName)]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Report report = await _context.Reports.FindAsync(id);
        //    if (report == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code", report.DevicePlaceId);
        //    ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code", report.DeviceStatusId);
        //    ViewData["ParentReportId"] = new SelectList(_context.Reports, "ReportId", "Code", report.ParentReportId);
        //    ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName", report.ReporterId);
        //    ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", report.WagonId);
        //    return View(report);
        //}

        //// POST: Reports/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ReportId,DateTimeCreated,DateTimeModified,ReporterId,Status,DeviceStatusId,ParentReportId,DevicePlaceId,WagonId")] Report report)
        //{
        //    if (id != report.ReportId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(report);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ReportExists(report.ReportId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code", report.DevicePlaceId);
        //    ViewData["DeviceStatusId"] = new SelectList(_context.DeviceStatus, "StatusId", "Code", report.DeviceStatusId);
        //    ViewData["ParentReportId"] = new SelectList(_context.Reports, "ReportId", "Code", report.ParentReportId);
        //    ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName", report.ReporterId);
        //    ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", report.WagonId);
        //    return View(report);
        //}

        // GET: Reports/Delete/5
        [Authorize(Roles = RolesList.Admin.RoleName)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report report = await _context.Reports
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
            Report report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }

        // GET: Reports/Upload
        [Authorize(Roles = RolesList.Admin.RoleName)]
        public IActionResult Upload()
        {
            return View();
        }

        private DateTime FromOldExcelDateTime(string InDate)
        {
            string persianDate = InDate;
            string[] userDateParts = persianDate.Split(new[] { "/" }, System.StringSplitOptions.None);
            int Year = int.Parse(userDateParts[2]);
            int Month = int.Parse(userDateParts[1]);
            int Day = int.Parse(userDateParts[0]);
            var persianDateTime = new PersianDateTime(Year, Month, Day);
            return persianDateTime.ToDateTime();
        }

        private class ReportValues
        {
            [DisplayName(Expressions.DateTime)]
            public string Date { get; set; }

            [DisplayName(Expressions.Wagon)]
            public string WagonNum { get; set; }

            [DisplayName(Expressions.DevicePlaces)]
            public string DevicePlaceCode { get; set; }

            [DisplayName(Expressions.UserName)]
            public string UserName { get; set; }

            [DisplayName(Expressions.DeviceStatus)]
            public string DeviceStatusCode { get; set; }
        }

        // POST: Reports/Upload
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[RequestSizeLimit(5000000)]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            IFormFile file = File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            List<Report> Reports = new List<Report>();
            Dictionary<string, Report> ChildReports = new Dictionary<string, Report>();
            List<int> LostReports = new List<int>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (ExcelPackage package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;
                    int[] ReportCells = { 0, 2, 3, 4, 6 };//بر اساس فایل گزارشهای شرکت ریل پرداز
                    int[] RepairCells = { 5, 7, 11 };//بر اساس فایل گزارشهای شرکت ریل پرداز
                    for (int i = 2; i < totalRows; i++)
                    {
                        string[] ReportCellValues = new string[ReportCells.Length];
                        bool SkipRow = false;
                        for (int c = 0; c < ReportCells.Length; c++)
                        {
                            object CellValue = ((object[,])(worksheet.Cells.Value))[i, ReportCells[c]];
                            if (CellValue == null)
                            {
                                SkipRow = true;
                                continue;
                            }
                            ReportCellValues[c] = CellValue.ToString();
                        }
                        if (SkipRow)
                        {
                            LostReports.Add(i + 1);
                            continue;
                        }
                        DateTime date;
                        try
                        {
                            date = FromOldExcelDateTime(ReportCellValues[0]);
                        }
                        catch
                        {
                            LostReports.Add(i + 1);
                            continue;
                        }
                        Wagon wagon = _context.Wagons.Where(w => w.Number == Convert.ToInt32(ReportCellValues[1])).FirstOrDefault();
                        DevicePlace devp = _context.DevicePlaces.Where(dp => dp.Code == ReportCellValues[2]).FirstOrDefault();
                        Reporter rep = _context.Reporters.Where(r => r.UserName == ReportCellValues[3]).FirstOrDefault();
                        DeviceStatus devS = _context.DeviceStatus.Where(ds => ds.Code == ReportCellValues[4]).FirstOrDefault();
                        string Code;
                        try
                        {
                            Code = GenerateReportCode(devp.DevicePlaceId, devS.StatusId, rep.ReporterId, wagon.WagonId, date);
                        }
                        catch
                        {
                            LostReports.Add(i + 1);
                            continue;
                        }
                        if (!_context.Reports.Any(r => r.Code == Code) &&
                            !Reports.Any(r => r.Code == Code))
                        {
                            Reports.Add(new Report
                            {
                                DateTimeCreated = date,
                                WagonId = wagon.WagonId,
                                DevicePlaceId = devp.DevicePlaceId,
                                DeviceStatusId = devS.StatusId,
                                ReporterId = rep.ReporterId,
                                Code = Code
                            });
                        }
                        //else
                        //    LostReports.Add(new ReportValues
                        //    {
                        //        Date = date,
                        //        WagonNum = ReportCellValues[1],
                        //        DevicePlaceCode = ReportCellValues[2],
                        //        UserName = ReportCellValues[3],
                        //        DeviceStatusCode = ReportCellValues[4]
                        //    });

                        string[] RepairCellValues = new string[RepairCells.Length];
                        bool NoChild = false;
                        for (int c = 0; c < RepairCells.Length; c++)
                        {
                            object CellValue = ((object[,])(worksheet.Cells.Value))[i, RepairCells[c]];
                            if (CellValue == null || CellValue.ToString().Length < 2)
                            {
                                NoChild = true;
                                break;
                            }
                            RepairCellValues[c] = CellValue.ToString();
                        }
                        if (NoChild)
                        {
                            continue;
                        }

                        Reporter repairer = _context.Reporters.Where(r => r.UserName == RepairCellValues[0]).FirstOrDefault();
                        DeviceStatus dsRep = _context.DeviceStatus.Where(ds => ds.Code == RepairCellValues[1]).FirstOrDefault();
                        DateTime RepDate = FromOldExcelDateTime(RepairCellValues[2]);
                        string RepCode;
                        try
                        {
                            RepCode = GenerateReportCode(devp.DevicePlaceId, dsRep.StatusId, repairer.ReporterId, wagon.WagonId, RepDate);
                        }
                        catch
                        {
                            LostReports.Add(i + 1);
                            continue;
                        }
                        ChildReports.Add(Code, new Report
                        {
                            DateTimeCreated = RepDate,
                            WagonId = wagon.WagonId,
                            DevicePlaceId = devp.DevicePlaceId,
                            DeviceStatusId = dsRep.StatusId,
                            ReporterId = repairer.ReporterId,
                            Code = RepCode
                        });
                    }
                }
            }
            _context.Reports.AddRange(Reports);
            await _context.SaveChangesAsync();

            string fileName = _env.WebRootPath + @"\Excel\LostReports.xlsx";

            FileInfo LostReportsFile = new FileInfo(fileName);
            if (LostReportsFile.Exists)
            {
                LostReportsFile.Delete();
            }

            using (ExcelPackage ExcelPackage = new ExcelPackage(LostReportsFile))
            {
                ExcelWorksheet worksheet = ExcelPackage.Workbook.Worksheets.Add(Expressions.LostReports);
                worksheet.Cells[1, 1].Value = "شماره ردیف";
                int row = 2;
                foreach (int r in LostReports)
                {
                    worksheet.Cells[row++, 1].Value = r.ToString();
                }
                ExcelPackage.Save();
            }

            List<Report> ChildReports2 = new List<Report>();
            foreach (KeyValuePair<string, Report> item in ChildReports)
            {
                Report parentReport = _context.Reports.Where(r => r.Code == item.Key).FirstOrDefault();
                if (parentReport == null)
                {
                    continue;//ParentReportIsLost
                }

                item.Value.ParentReportId = parentReport.ReportId;

                if (!_context.Reports.Any(r => r.Code == item.Value.Code))
                    ChildReports2.Add(item.Value);
                else
                    continue;
                _context.Reports.Update(parentReport);
            }
            _context.Reports.AddRange(ChildReports2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public FileResult DownloadLostReports(string filename)
        {
            return PhysicalFile(filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Expressions.LostReports + ".xlsx");
        }

        public IActionResult Download()
        {
            string fileName = _env.WebRootPath + @"\Excel\Reports.xlsx";

            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
                file.Delete();
            using (ExcelPackage ExcelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = ExcelPackage.Workbook.Worksheets.Add(Expressions.Reports);
                worksheet.View.RightToLeft = true;
                List<Report> reports = _context.Reports.ToList();
                int row = 1;
                int col = 1;
                worksheet.Cells[row, col++].Value = Expressions.DateTime;
                worksheet.Cells[row, col++].Value = Expressions.Wagon;
                worksheet.Cells[row, col++].Value = Expressions.Code + Expressions.DevicePlaces;
                worksheet.Cells[row, col++].Value = Expressions.DevicePlaces;
                worksheet.Cells[row, col++].Value = Expressions.Reporter;
                worksheet.Cells[row, col++].Value = Expressions.DeviceStatus;
                worksheet.Cells[row, col++].Value = Expressions.Status;
                worksheet.Cells[row, col++].Value = Expressions.Site;
                foreach (Report r in reports)
                {
                    if (r.ParentReport != null) continue;
                    col = 1;
                    row++;
                    worksheet.Cells[row, col++].Value = r.DateTimeCreated.ToString("yy/MM/dd");
                    worksheet.Cells[row, col++].Value = r.Wagon.Name;
                    worksheet.Cells[row, col++].Value = r.DevicePlace.Code;
                    worksheet.Cells[row, col++].Value = r.DevicePlace.Description;
                    worksheet.Cells[row, col++].Value = r.Reporter.UserName;
                    worksheet.Cells[row, col++].Value = r.DeviceStatus.Code;
                    worksheet.Cells[row, col++].Value = r.DeviceStatus.Text;
                    col++;
                    if (r.Site != null)
                        worksheet.Cells[row, col].Value = r.Site.Name;
                    foreach (Report cr in r.AppendixReports)
                    {
                        worksheet.Cells[row, col++].Value = cr.DateTimeCreated.ToString("yy/MM/dd");
                        worksheet.Cells[1, col].Value = Expressions.DateTime;
                        worksheet.Cells[row, col++].Value = cr.Reporter.UserName;
                        worksheet.Cells[1, col].Value = Expressions.Reporter;
                        worksheet.Cells[row, col++].Value = cr.DeviceStatus.Code;
                        worksheet.Cells[1, col].Value = Expressions.Code + Expressions.Status;
                        worksheet.Cells[row, col++].Value = cr.DeviceStatus.Text;
                        worksheet.Cells[1, col].Value = Expressions.DeviceStatus;
                    }
                }
                //worksheet.Cells["A1"].LoadFromCollection(reports, true, TableStyles.Medium25);

                var range = worksheet.Cells[worksheet.Dimension.Address];
                var table = worksheet.Tables.Add(range, "Reports");
                table.TableStyle = TableStyles.Medium25;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                ExcelPackage.Save();
            }
            return PhysicalFile(fileName, "	application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Expressions.Reports + ".xlsx");
        }
    }
}