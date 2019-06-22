using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2;
using Syncfusion.EJ2.Navigations;
using VinarishMvc.Data;
using VinarishMvc.Models;

namespace VinarishMvc.Controllers
{
    public class DeviceManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceManagementController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class SubmitModel
        {
            public string Name { get; set; }
            public int PrimaryKey { get; set; }
            public string Value { get; set; }
        }
        public SubmitModel UpdateData([FromBody]SubmitModel payload)
        {
            if (payload.Name == "DevicePlace")
            {
                var devicePlace = _context.DevicePlaces.Find(payload.PrimaryKey);
                devicePlace.Description = payload.Value;
                _context.Update(devicePlace);
                _context.SaveChanges();
            }
            return payload;
        }
        // GET: DeviceManagement
        public async Task<IActionResult> Index()
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.DeviceTypes = await _context.DeviceTypes.ToListAsync();
            ViewBag.DevicePlaces = _context.DevicePlaces.Include(d => d.DeviceType).ToList();

            ViewBag.modalData = new { placeholder = "Enter employee name" };
            ViewBag.value = "Andrew";
            return View();
        }

        // GET: DeviceManagement/Details/5
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

        // GET: DeviceManagement/Create
        public IActionResult Create()
        {
            ViewData["DeviceTypeId"] = new SelectList(_context.DeviceTypes, "DeviceTypeId", "Name");
            return View();
        }

        // POST: DeviceManagement/Create
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

        // GET: DeviceManagement/Edit/5
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

        // POST: DeviceManagement/Edit/5
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

        // GET: DeviceManagement/Delete/5
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

        // POST: DeviceManagement/Delete/5
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
    }
}
