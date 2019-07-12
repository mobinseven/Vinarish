using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using VinarishMvc.Areas.Authentication.Data;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    [Authorize(Roles = RolesList.Admin.RoleName)]
    public class DeviceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public DeviceTypesController(
            IHostingEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        // GET: DeviceTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeviceTypes.Include(d => d.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeviceTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.DeviceTypeId == id);
            if (deviceType == null)
            {
                return NotFound();
            }

            return View(deviceType);
        }

        // GET: DeviceTypes/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: DeviceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceTypeId,Name,DepartmentId")] DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                deviceType.DeviceTypeId = Guid.NewGuid();
                _context.Add(deviceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", deviceType.DepartmentId);
            return View(deviceType);
        }

        // GET: DeviceTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", deviceType.DepartmentId);
            return View(deviceType);
        }

        // POST: DeviceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceTypeId,Name,DepartmentId")] DeviceType deviceType)
        {
            if (id != deviceType.DeviceTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deviceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceTypeExists(deviceType.DeviceTypeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", deviceType.DepartmentId);
            return View(deviceType);
        }

        // GET: DeviceTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.DeviceTypeId == id);
            if (deviceType == null)
            {
                return NotFound();
            }

            return View(deviceType);
        }

        // POST: DeviceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceTypeExists(Guid id)
        {
            return _context.DeviceTypes.Any(e => e.DeviceTypeId == id);
        }

        // GET: DeviceTypes/Upload
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

        // POST: DeviceTypes/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(5000000)]
        public async Task<IActionResult> Upload(UploadViewModel model)
        {
            IFormFile file = model.File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            List<DeviceType> DeviceTypes = new List<DeviceType>();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;


                    for (int i = 1; i < totalRows; i++)
                    {
                        var name = ((object[,])(worksheet.Cells.Value))[i, 0].ToString();
                        if (_context.DeviceTypes.Any(dt => dt.Name == name)) continue;
                        DeviceTypes.Add(new DeviceType
                        {
                            Name = name,
                            DepartmentId = model.DepartmentId
                        });
                    }
                }
            }

            _context.DeviceTypes.AddRange(DeviceTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private class DeviceTypeTableType
        {
            [DisplayName(Expressions.DeviceTypes)]
            public string Name { get; set; }
        }
        public FileResult Download()
        {
            string fileName = _env.WebRootPath + @"\Excel\DeviceTypes.xlsx";

            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
                file.Delete();
            using (ExcelPackage ExcelPackage = new ExcelPackage(file))
            {
                IList<DeviceTypeTableType> DeviceTypes = _context.DeviceTypes.Select(dt => new DeviceTypeTableType { Name = dt.Name}).ToList();
                ExcelWorksheet worksheet = ExcelPackage.Workbook.Worksheets.Add(Expressions.DeviceTypes);
                worksheet.Cells["A1"].LoadFromCollection(DeviceTypes, true, TableStyles.Medium25);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                ExcelPackage.Save();

            }
            return PhysicalFile(fileName, "	application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Expressions.DeviceTypes + ".xlsx");
        }

    }
}
