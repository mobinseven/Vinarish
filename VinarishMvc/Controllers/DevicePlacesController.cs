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
    public class DevicePlacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public DevicePlacesController(
            IHostingEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        // GET: DevicePlaces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DevicePlaces.Include(d => d.DeviceType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DevicePlaces/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devicePlace = await _context.DevicePlaces
                .Include(d => d.DeviceType)
                .FirstOrDefaultAsync(m => m.DevicePlaceId == id);
            if (devicePlace == null)
            {
                return NotFound();
            }

            return View(devicePlace);
        }

        // GET: DevicePlaces/Create
        public IActionResult Create()
        {
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name");
            return View();
        }

        // POST: DevicePlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DevicePlaceId,Code,Description,DeviceTypeId")] DevicePlace devicePlace)
        {
            if (ModelState.IsValid)
            {
                devicePlace.DevicePlaceId = Guid.NewGuid();
                _context.Add(devicePlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", devicePlace.DeviceTypeId);
            return View(devicePlace);
        }

        // GET: DevicePlaces/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devicePlace = await _context.DevicePlaces.FindAsync(id);
            if (devicePlace == null)
            {
                return NotFound();
            }
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", devicePlace.DeviceTypeId);
            return View(devicePlace);
        }

        // POST: DevicePlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DevicePlaceId,Code,Description,DeviceTypeId")] DevicePlace devicePlace)
        {
            if (id != devicePlace.DevicePlaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devicePlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevicePlaceExists(devicePlace.DevicePlaceId))
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
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", devicePlace.DeviceTypeId);
            return View(devicePlace);
        }

        // GET: DevicePlaces/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devicePlace = await _context.DevicePlaces
                .Include(d => d.DeviceType)
                .FirstOrDefaultAsync(m => m.DevicePlaceId == id);
            if (devicePlace == null)
            {
                return NotFound();
            }

            return View(devicePlace);
        }

        // POST: DevicePlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var devicePlace = await _context.DevicePlaces.FindAsync(id);
            _context.DevicePlaces.Remove(devicePlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevicePlaceExists(Guid id)
        {
            return _context.DevicePlaces.Any(e => e.DevicePlaceId == id);
        }

        // GET: DevicePlaces/Upload
        public IActionResult Upload()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        public class UploadViewModel
        {
            public Guid DepartmentId { get; set; }
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
            List<DevicePlace> DevicePlaces = new List<DevicePlace>();
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
                        if (_context.DevicePlaces.Any(ds => ds.Code == code)) continue;
                        var text = (string)((object[,])(worksheet.Cells.Value))[i, 1];
                        if (_context.DevicePlaces.Any(ds => ds.Description == text)) continue;
                        var dtidCell = (string)((object[,])(worksheet.Cells.Value))[i, 2];
                        if (dtidCell == null) continue;
                        var dt = _context.DeviceTypes.Where(x => x.Name == dtidCell).FirstOrDefault();
                        if (dt == null) continue;
                        DevicePlaces.Add(new DevicePlace
                        {
                            DeviceTypeId = dt.DeviceTypeId,
                            Code = code,
                            Description = text
                        });
                    }
                }
            }

            _context.DevicePlaces.AddRange(DevicePlaces);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private class DevicePlaceTableType
        {
            [DisplayName(Expressions.Code)]
            public string Code { get; set; }
            [DisplayName(Expressions.DevicePlaces)]
            public string Description { get; set; }
            [DisplayName(Expressions.DeviceTypes)]
            public string DeviceType { get; set; }
        }
        public FileResult Download()
        {
            string fileName = _env.WebRootPath + @"\Excel\DevicePlaces.xlsx";

            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
                file.Delete();
            using (ExcelPackage ExcelPackage = new ExcelPackage(file))
            {
                IList<DevicePlaceTableType> DevicePlaces = _context.DevicePlaces.Select(dp => new DevicePlaceTableType { Code = dp.Code, Description = dp.Description, DeviceType = dp.DeviceType.Name }).ToList();
                ExcelWorksheet worksheet = ExcelPackage.Workbook.Worksheets.Add(Expressions.DevicePlaces);
                worksheet.Cells["A1"].LoadFromCollection(DevicePlaces, true, TableStyles.Medium25);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                ExcelPackage.Save();

            }
            return PhysicalFile(fileName, "	application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Expressions.DevicePlaces + ".xlsx");
        }
    }
}
