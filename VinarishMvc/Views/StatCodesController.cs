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
using VinarishMvc.Models;

namespace VinarishMvc.Views
{
    public class StatCodesController : Controller
    {
        private readonly VinarishContext _context;

        public StatCodesController(VinarishContext context)
        {
            _context = context;
        }

        // GET: StatCodes
        public async Task<IActionResult> Index()
        {
            var vinarishContext = _context.StatCode.Include(s => s.Cat).OrderBy(x => x.Cat.CategoryName).OrderBy(x => Utilities.PadNumbers(x.Code));
            return View(await vinarishContext.ToListAsync());
        }

        // GET: StatCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statCode = await _context.StatCode
                .Include(s => s.Cat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statCode == null)
            {
                return NotFound();
            }

            return View(statCode);
        }

        // GET: StatCodes/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName");
            return View();
        }

        // POST: StatCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Text,CatId")] StatCode statCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName", statCode.CatId);
            return View(statCode);
        }

        // GET: StatCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statCode = await _context.StatCode.FindAsync(id);
            if (statCode == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName", statCode.CatId);
            return View(statCode);
        }

        // POST: StatCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Text,CatId")] StatCode statCode)
        {
            if (id != statCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatCodeExists(statCode.Id))
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
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName", statCode.CatId);
            return View(statCode);
        }

        // GET: StatCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statCode = await _context.StatCode
                .Include(s => s.Cat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statCode == null)
            {
                return NotFound();
            }

            return View(statCode);
        }

        // POST: StatCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statCode = await _context.StatCode.FindAsync(id);
            _context.StatCode.Remove(statCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatCodeExists(int id)
        {
            return _context.StatCode.Any(e => e.Id == id);
        }

        // GET: StatCodes/Upload
        public IActionResult Upload()
        {
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName");
            return View();
        }
        // POST: StatCodes/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(StatCodes.UploadViewModel model)
        {
            IFormFile file = model.File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("Index");
            }
            List<StatCode> StatCodes = new List<StatCode>();

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;


                    for (int i = 0; i < totalRows; i++)
                    {
                        StatCodes.Add(new StatCode
                        {
                            Code = ((object[,])(worksheet.Cells.Value))[i, 0].ToString(),
                            Text = ((object[,])(worksheet.Cells.Value))[i, 1].ToString(),
                            CatId = model.CatId
                        });
                    }
                }
            }

            _context.StatCode.AddRange(StatCodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
