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
using VinarishMvc.Models.Syncfusion;

namespace VinarishMvc.Controllers
{
    public class WagonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WagonsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Wagons1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wagons.ToListAsync());
        }

        // GET: Wagons1/Details/5
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
                .OrderByDescending(r=>r.DateTimeCreated)
                .ToList();
            if (wagon == null)
            {
                return NotFound();
            }

            return View(wagon);
        }

        // GET: Wagons1/Create
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

        // GET: Wagons
        public ActionResult IndexSync()
        {
            ViewBag.dataSource = _context.Wagons.ToList();
            return View();
        }


        // POST: Wagons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<Wagon> payload)
        {
            _context.Add(payload.value);
            await _context.SaveChangesAsync();
            ViewBag.dataSource = await _context.Wagons.ToListAsync();
            return Json(payload.value);
        }

        // POST: Wagons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("value")][FromBody]CrudViewModel<Wagon> payload)
        {
            if (ModelState.IsValid)
            {
                _context.Update(payload.value);
                await _context.SaveChangesAsync();
                // TODO: DbUpdateConcurrencyException
                return RedirectToAction(nameof(IndexSync));
            }
            return View(payload.value);
        }
        // POST: Wagons/Remove/5
        [HttpPost, ActionName("Remove")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([Bind("key")][FromBody]CrudViewModel<Wagon> payload)
        {
            var Wagon = await _context.Wagons.FindAsync(Convert.ToInt32(payload.key));
            _context.Wagons.Remove(Wagon);
            await _context.SaveChangesAsync();
            var data = _context.Wagons.ToList();
            return Json(data);
        }
        // POST: Wagons/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            IFormFile file = File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(IndexSync));
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
                            Number = Convert.ToInt32(((object[,])(worksheet.Cells.Value))[i, 0]),
                            Name = ((object[,])(worksheet.Cells.Value))[i, 1].ToString()
                        });
                    }
                }
            }

            _context.Wagons.AddRange(Wagons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexSync));
        }
    }
}
