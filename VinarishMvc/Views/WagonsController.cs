using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Models;

namespace VinarishMvc.Views
{
    public class WagonsController : Controller
    {
        private readonly VinarishContext _context;

        public WagonsController(VinarishContext context)
        {
            _context = context;
        }

        // GET: Wagons
        public async Task<IActionResult> Index()
        {
            var vinarishContext = _context.Wagon.Include(w => w.Train);
            return View(await vinarishContext.ToListAsync());
        }

        // GET: Wagons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagon = await _context.Wagon
                .Include(w => w.Train)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wagon == null)
            {
                return NotFound();
            }

            return View(wagon);
        }

        // GET: Wagons/Create
        public IActionResult Create()
        {
            ViewData["TrainId"] = new SelectList(_context.Train, "Id", "TrainId");
            return View();
        }

        // POST: Wagons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WagonId,TrainId")] Wagon wagon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wagon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainId"] = new SelectList(_context.Train, "Id", "TrainId", wagon.TrainId);
            return View(wagon);
        }

        // GET: Wagons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagon = await _context.Wagon.FindAsync(id);
            if (wagon == null)
            {
                return NotFound();
            }
            ViewData["TrainId"] = new SelectList(_context.Train, "Id", "TrainId", wagon.TrainId);
            return View(wagon);
        }

        // POST: Wagons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WagonId,TrainId")] Wagon wagon)
        {
            if (id != wagon.Id)
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
                    if (!WagonExists(wagon.Id))
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
            ViewData["TrainId"] = new SelectList(_context.Train, "Id", "TrainId", wagon.TrainId);
            return View(wagon);
        }

        // GET: Wagons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagon = await _context.Wagon
                .Include(w => w.Train)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var wagon = await _context.Wagon.FindAsync(id);
            _context.Wagon.Remove(wagon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WagonExists(int id)
        {
            return _context.Wagon.Any(e => e.Id == id);
        }
    }
}
