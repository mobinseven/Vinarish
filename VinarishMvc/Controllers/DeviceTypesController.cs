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
    public class DeviceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeviceTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeviceTypes.Include(d => d.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeviceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.DeviceTypeId == id);
            if (deviceType == null)
            {
                return NotFound();
            }

            return View(deviceType);
        }

        // GET: DeviceTypes/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: DeviceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceTypeId,Name,DepartmentId")] DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deviceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", deviceType.DepartmentId);
            return View(deviceType);
        }

        // GET: DeviceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", deviceType.DepartmentId);
            return View(deviceType);
        }

        // POST: DeviceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeviceTypeId,Name,DepartmentId")] DeviceType deviceType)
        {
            if (id != deviceType.DeviceTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deviceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceTypeExists(deviceType.DeviceTypeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", deviceType.DepartmentId);
            return View(deviceType);
        }

        // GET: DeviceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.DeviceTypeId == id);
            if (deviceType == null)
            {
                return NotFound();
            }

            return View(deviceType);
        }

        // POST: DeviceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceTypeExists(int id)
        {
            return _context.DeviceTypes.Any(e => e.DeviceTypeId == id);
        }
    }
}
