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
    public class DevicePlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicePlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: DevicePlaces
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.DevicePlaces.Include(d => d.DeviceType);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        // GET: DevicePlaces
        public async Task<IActionResult> IndexSync()
        {
            var applicationDbContext = _context.DevicePlaces;//.Include(d => d.DeviceType);
            ViewData["dataSource"] = _context.DeviceTypes.FirstOrDefault();
            return View(await applicationDbContext.ToListAsync());
        }
        // POST: DevicePlaces/Create
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<DevicePlace> payload)
        {
            _context.Add(payload.value);
            await _context.SaveChangesAsync();
            ViewBag.dataSource = await _context.DevicePlaces.ToListAsync();
            return Json(payload.value);
        }

        // POST: DevicePlaces/Edit
        [HttpPost]
        public async Task<IActionResult> Update([Bind("value")][FromBody]CrudViewModel<DevicePlace> payload)
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
        // POST: DevicePlaces/Remove
        [HttpPost, ActionName("Remove")]
        public async Task<IActionResult> Remove([Bind("key")][FromBody]CrudViewModel<DevicePlace> payload)
        {
            var DevicePlace = await _context.DevicePlaces.FindAsync(payload.key);
            _context.DevicePlaces.Remove(DevicePlace);
            await _context.SaveChangesAsync();
            var data = _context.DevicePlaces.ToList();
            return Json(data);
        }
        // POST: DevicePlaces/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            IFormFile file = File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(IndexSync));
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
            return RedirectToAction(nameof(IndexSync));
        }
    }
}
