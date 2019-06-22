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
