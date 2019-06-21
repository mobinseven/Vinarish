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
    public class DevicePlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicePlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DevicePlaces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DevicePlaces.Include(d => d.DeviceType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DevicePlaces/Details/5
        public async Task<IActionResult> Details(int? id)
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
                _context.Add(devicePlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name", devicePlace.DeviceTypeId);
            return View(devicePlace);
        }

        // GET: DevicePlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("DevicePlaceId,Code,Description,DeviceTypeId")] DevicePlace devicePlace)
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
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var devicePlace = await _context.DevicePlaces.FindAsync(id);
            _context.DevicePlaces.Remove(devicePlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevicePlaceExists(int id)
        {
            return _context.DevicePlaces.Any(e => e.DevicePlaceId == id);
        }
        // GET: DevicePlaces
        public ActionResult IndexSync()
        {
            ViewBag.dataSource = _context.DevicePlaces.ToList();
            return View();
        }
        // POST: DevicePlaces/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            IFormFile file = File;
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
                        DevicePlaces.Add(new DevicePlace
                        {
                            DeviceTypeId = _context.DeviceTypes.Where(x => x.Name.Contains(((object[,])(worksheet.Cells.Value))[i, 2].ToString())).FirstOrDefault().DeviceTypeId,
                            Code = ((object[,])(worksheet.Cells.Value))[i, 0].ToString(),
                            Description = ((object[,])(worksheet.Cells.Value))[i, 1].ToString()
                        });
                    }
                }
            }

            _context.DevicePlaces.AddRange(DevicePlaces);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
