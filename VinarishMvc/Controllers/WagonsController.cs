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
