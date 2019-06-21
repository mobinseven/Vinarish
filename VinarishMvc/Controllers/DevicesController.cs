﻿using System;
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
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Devices.Include(d => d.DevicePlace).Include(d => d.Wagon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.DevicePlace)
                .Include(d => d.Wagon)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code");
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,WagonId,Serial,GuaranteeDate,DevicePlaceId")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code", device.DevicePlaceId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", device.WagonId);
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code", device.DevicePlaceId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", device.WagonId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeviceId,WagonId,Serial,GuaranteeDate,DevicePlaceId")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.DeviceId))
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
            ViewData["DevicePlaceId"] = new SelectList(_context.DevicePlaces, "DevicePlaceId", "Code", device.DevicePlaceId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", device.WagonId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.DevicePlace)
                .Include(d => d.Wagon)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.DeviceId == id);
        }

        // GET: Devices
        public ActionResult IndexSync()
        {
            ViewBag.dataSource = _context.Devices.ToList();
            return View();
        }
        // POST: Devices/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            IFormFile file = File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(IndexSync));
            }
            List<Device> Devices = new List<Device>();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;

                    List<Wagon> Wagons = _context.Wagons.ToList();
                    foreach (Wagon w in Wagons)
                    {
                        for (int i = 1; i < totalRows; i++)
                        {
                            Devices.Add(new Device
                            {
                                DevicePlaceId = _context.DevicePlaces.Where(x => x.DeviceType.Name.Contains(((object[,])(worksheet.Cells.Value))[i, 2].ToString())).FirstOrDefault().DevicePlaceId,
                                WagonId = w.WagonId
                            });
                        }
                    }
                }
            }

            _context.Devices.AddRange(Devices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexSync));
        }
    }
}
