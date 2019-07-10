using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using VinarishMvc.Areas.Authentication.Data;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    [Authorize(Roles = RolesList.User.RoleName)]
    public class WagonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WagonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wagons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wagons.OrderBy(w => w.Name).ToListAsync());
        }

        // GET: Wagons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagon = await _context.Wagons
                .FirstOrDefaultAsync(m => m.WagonId == id);
            ViewBag.Reports = _context.Reports
                .Where(r => r.WagonId == id)
                .Include(r => r.DevicePlace)
                .Include(r => r.DeviceStatus)
                .Include(r => r.Reporter)
                .OrderByDescending(r => r.DateTimeCreated)
                .ToList();
            if (wagon == null)
            {
                return NotFound();
            }

            return View(wagon);
        }

        [Authorize(Roles = RolesList.Admin.RoleName)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wagons1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WagonId,Number,Name")] Wagon wagon)
        {
            if (ModelState.IsValid)
            {
                wagon.WagonId = Guid.NewGuid();
                _context.Add(wagon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wagon);
        }

        // GET: Wagons1/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagon = await _context.Wagons.FindAsync(id);
            if (wagon == null)
            {
                return NotFound();
            }
            return View(wagon);
        }

        // POST: Wagons1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesList.Admin.RoleName)]
        public async Task<IActionResult> Edit(Guid id, [Bind("WagonId,Number,Name")] Wagon wagon)
        {
            if (id != wagon.WagonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wagon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WagonExists(wagon.WagonId))
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
            return View(wagon);
        }

        // GET: Wagons1/Delete/5
        [Authorize(Roles = RolesList.Admin.RoleName)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagon = await _context.Wagons
                .FirstOrDefaultAsync(m => m.WagonId == id);
            if (wagon == null)
            {
                return NotFound();
            }

            return View(wagon);
        }

        // POST: Wagons1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var wagon = await _context.Wagons.FindAsync(id);
            _context.Wagons.Remove(wagon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WagonExists(Guid id)
        {
            return _context.Wagons.Any(e => e.WagonId == id);
        }

        // POST: Wagons/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesList.Admin.RoleName)]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            IFormFile file = File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            List<Wagon> Wagons = new List<Wagon>();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;

                    for (int i = 1; i < totalRows; i++)
                    {
                        Wagons.Add(new Wagon
                        {
                            Name = ((object[,])(worksheet.Cells.Value))[i, 1].ToString()
                        });
                    }
                }
            }

            _context.Wagons.AddRange(Wagons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}