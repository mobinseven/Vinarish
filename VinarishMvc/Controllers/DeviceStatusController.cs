using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    public class DeviceStatusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public DeviceStatusController(
        IHostingEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        // GET: DeviceStatus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeviceStatus.Include(d => d.DeviceType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeviceStatus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceStatus = await _context.DeviceStatus
                .Include(d => d.DeviceType)
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (deviceStatus == null)
            {
                return NotFound();
            }

            return View(deviceStatus);
        }

        // GET: DeviceStatus/Create
        public IActionResult Create()
        {
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name");
            return View();
        }

        // POST: DeviceStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,Code,Text,DeviceStatusType,DeviceTypeId")] DeviceStatus deviceStatus)
        {
            deviceStatus.StatusId = Guid.NewGuid();
            _context.Add(deviceStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", deviceStatus.DeviceTypeId);
            //return View(deviceStatus);
        }

        // GET: DeviceStatus/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceStatus = await _context.DeviceStatus.FindAsync(id);
            if (deviceStatus == null)
            {
                return NotFound();
            }
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", deviceStatus.DeviceTypeId);
            return View(deviceStatus);
        }

        // POST: DeviceStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StatusId,Code,Text,DeviceStatusType,DeviceTypeId")] DeviceStatus deviceStatus)
        {
            if (id != deviceStatus.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deviceStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceStatusExists(deviceStatus.StatusId))
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
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", deviceStatus.DeviceTypeId);
            return View(deviceStatus);
        }

        // GET: DeviceStatus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceStatus = await _context.DeviceStatus
                .Include(d => d.DeviceType)
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (deviceStatus == null)
            {
                return NotFound();
            }

            return View(deviceStatus);
        }

        // POST: DeviceStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deviceStatus = await _context.DeviceStatus.FindAsync(id);
            _context.DeviceStatus.Remove(deviceStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceStatusExists(Guid id)
        {
            return _context.DeviceStatus.Any(e => e.StatusId == id);
        }

        // GET: DeviceStatus/Upload
        public IActionResult Upload()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewData["DeviceStatusType"] = new SelectList(Enum.GetValues(typeof(DeviceStatusType)).Cast<DeviceStatusType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");
            return View();
        }

        public class UploadViewModel
        {
            public Guid DepartmentId { get; set; }
            public DeviceStatusType DeviceStatusType { get; set; }
            public IFormFile File { get; set; }
        }
        // POST: DevicePlaces/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadViewModel model)
        {
            IFormFile file = model.File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            List<DeviceStatus> DeviceStatus = new List<DeviceStatus>();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;

                    for (int i = 1; i < totalRows; i++)
                    {
                        var code = (string)((object[,])(worksheet.Cells.Value))[i, 0];
                        if (_context.DeviceStatus.Any(ds => ds.Code == code)) continue;
                        var text = (string)((object[,])(worksheet.Cells.Value))[i, 1];
                        if (_context.DeviceStatus.Any(ds => ds.Text == text)) continue;
                        var dtidCell = (string)((object[,])(worksheet.Cells.Value))[i, 2];
                        if (dtidCell == null) continue;
                        var dt = _context.DeviceTypes.Where(x => x.Name == dtidCell).FirstOrDefault();
                        if (dt == null) continue;
                        DeviceStatus.Add(new DeviceStatus
                        {
                            DeviceTypeId = dt.DeviceTypeId,
                            Code = code,
                            Text = text,
                            DeviceStatusType = model.DeviceStatusType
                        });
                    }
                }
            }

            _context.DeviceStatus.AddRange(DeviceStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private class DeviceStatusTableType
        {
            [DisplayName(Expressions.Code)]
            public string Code { get; set; }
            //[DisplayName(Expressions.DeviceStatusMalfunction)]
            public string Text { get; set; }
            [DisplayName(Expressions.DeviceTypes)]
            public string DeviceType { get; set; }
        }
        public FileResult Download()
        {
            string fileName = _env.WebRootPath + @"\Excel\DeviceStatus.xlsx";

            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
                file.Delete();
            using (ExcelPackage ExcelPackage = new ExcelPackage(file))
            {
                IList<DeviceStatusTableType> DeviceStatusMal = _context.DeviceStatus
                    .Where(ds => ds.DeviceStatusType == DeviceStatusType.Malfunction)
                    .Select(ds => new DeviceStatusTableType { Code = ds.Code, Text = ds.Text, DeviceType = ds.DeviceType.Name })
                    .ToList();
                ExcelWorksheet worksheet = ExcelPackage.Workbook.Worksheets.Add(Expressions.DeviceStatusMalfunction);
                worksheet.Cells["A1"].LoadFromCollection(DeviceStatusMal, true, TableStyles.Medium25);
                worksheet.Cells["B2"].Value = Expressions.DeviceStatusMalfunction;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                IList<DeviceStatusTableType> DeviceStatusRep = _context.DeviceStatus
                    .Where(ds => ds.DeviceStatusType == DeviceStatusType.Repair)
                    .Select(ds => new DeviceStatusTableType { Code = ds.Code, Text = ds.Text, DeviceType = ds.DeviceType.Name })
                    .ToList();
                worksheet = ExcelPackage.Workbook.Worksheets.Add(Expressions.DeviceStatusRepair);
                worksheet.Cells["A1"].LoadFromCollection(DeviceStatusRep, true, TableStyles.Medium25);
                worksheet.Cells["B2"].Value = Expressions.DeviceStatusRepair;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();


                IList<DeviceStatusTableType> DeviceStatusNep = _context.DeviceStatus
                    .Where(ds => ds.DeviceStatusType == DeviceStatusType.Unrepairable)
                    .Select(ds => new DeviceStatusTableType { Code = ds.Code, Text = ds.Text, DeviceType = ds.DeviceType.Name })
                    .ToList();
                worksheet = ExcelPackage.Workbook.Worksheets.Add(Expressions.DeviceStatusNRepair);
                worksheet.Cells["A1"].LoadFromCollection(DeviceStatusNep, true, TableStyles.Medium25);
                worksheet.Cells["B2"].Value = Expressions.DeviceStatusNRepair;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                ExcelPackage.Save();

            }
            return PhysicalFile(fileName, "	application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Expressions.DeviceStatus + ".xlsx");
        }
    }
}
