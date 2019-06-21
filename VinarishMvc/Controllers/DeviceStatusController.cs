using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            return View(await _context.DeviceStatus.ToListAsync());
        }

        // GET: DeviceStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceStatus = await _context.DeviceStatus
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
            return View();
        }

        // POST: DeviceStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,Code,Text,DeviceStatusType")] DeviceStatus deviceStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deviceStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deviceStatus);
        }

        // GET: DeviceStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(deviceStatus);
        }

        // POST: DeviceStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,Code,Text,DeviceStatusType")] DeviceStatus deviceStatus)
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
            return View(deviceStatus);
        }

        // GET: DeviceStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceStatus = await _context.DeviceStatus
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deviceStatus = await _context.DeviceStatus.FindAsync(id);
            _context.DeviceStatus.Remove(deviceStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceStatusExists(int id)
        {
            return _context.DeviceStatus.Any(e => e.StatusId == id);
        }
    }
}
