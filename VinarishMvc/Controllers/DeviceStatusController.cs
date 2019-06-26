using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    public class DeviceStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceStatusController(ApplicationDbContext context)
        {
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
            if (ModelState.IsValid)
            {
                deviceStatus.StatusId = Guid.NewGuid();
                _context.Add(deviceStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", deviceStatus.DeviceTypeId);
            return View(deviceStatus);
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

        // POST: DevicePlaces/UploadError
        [HttpPost, ActionName("UploadError")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadError(IFormFile File)
        {
            IFormFile file = File;
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
                        DeviceStatus.Add(new DeviceStatus
                        {
                            DeviceTypeId = _context.DeviceTypes.Where(x => x.Name.Contains(((object[,])(worksheet.Cells.Value))[i, 2].ToString())).FirstOrDefault().DeviceTypeId,
                            Code = ((object[,])(worksheet.Cells.Value))[i, 0].ToString(),
                            Text = ((object[,])(worksheet.Cells.Value))[i, 1].ToString(),
                            DeviceStatusType = DeviceStatusType.Malfunction
                        });
                    }
                }
            }

            _context.DeviceStatus.AddRange(DeviceStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: DevicePlaces/UploadRepair
        [HttpPost, ActionName("UploadRepair")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadRepair(IFormFile File)
        {
            IFormFile file = File;
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
                        DeviceStatus.Add(new DeviceStatus
                        {
                            DeviceTypeId = _context.DeviceTypes.Where(x => x.Name.Contains(((object[,])(worksheet.Cells.Value))[i, 2].ToString())).FirstOrDefault().DeviceTypeId,
                            Code = ((object[,])(worksheet.Cells.Value))[i, 0].ToString(),
                            Text = ((object[,])(worksheet.Cells.Value))[i, 1].ToString(),
                            DeviceStatusType = DeviceStatusType.Repair
                        });
                    }
                }
            }

            _context.DeviceStatus.AddRange(DeviceStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
