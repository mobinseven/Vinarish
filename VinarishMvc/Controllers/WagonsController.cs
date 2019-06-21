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
            return View(await _context.Wagons.ToListAsync());
        }

        // GET: Wagons/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Wagons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wagons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WagonId,Number,Name")] Wagon wagon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wagon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wagon);
        }

        // GET: Wagons/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Wagons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WagonId,Number,Name")] Wagon wagon)
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

        // GET: Wagons/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Wagons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wagon = await _context.Wagons.FindAsync(id);
            _context.Wagons.Remove(wagon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WagonExists(int id)
        {
            return _context.Wagons.Any(e => e.WagonId == id);
        }
    }
}
