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
    public class WagonTripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WagonTripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WagonTrips
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WagonTrips.Include(w => w.TrainTrip).Include(w => w.Wagon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WagonTrips/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagonTrip = await _context.WagonTrips
                .Include(w => w.TrainTrip)
                .ThenInclude(tt=>tt.Train)
                .Include(w => w.Wagon)
                .Include(w=>w.Reports)
                .FirstOrDefaultAsync(m => m.WagonTripId == id);
            if (wagonTrip == null)
            {
                return NotFound();
            }

            return View(wagonTrip);
        }

        // GET: WagonTrips/Create
        public IActionResult Create()
        {
            ViewData["TrainTripId"] = new SelectList(_context.TrainTrips, "TrainTripId", "TrainTripId");
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name");
            return View();
        }

        // POST: WagonTrips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WagonTripId,WagonId,TrainTripId")] WagonTrip wagonTrip)
        {
            if (ModelState.IsValid)
            {
                wagonTrip.WagonTripId = Guid.NewGuid();
                _context.Add(wagonTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainTripId"] = new SelectList(_context.TrainTrips, "TrainTripId", "TrainTripId", wagonTrip.TrainTripId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", wagonTrip.WagonId);
            return View(wagonTrip);
        }

        // GET: WagonTrips/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagonTrip = await _context.WagonTrips.FindAsync(id);
            if (wagonTrip == null)
            {
                return NotFound();
            }
            ViewData["TrainTripId"] = new SelectList(_context.TrainTrips, "TrainTripId", "TrainTripId", wagonTrip.TrainTripId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", wagonTrip.WagonId);
            return View(wagonTrip);
        }

        // POST: WagonTrips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WagonTripId,WagonId,TrainTripId")] WagonTrip wagonTrip)
        {
            if (id != wagonTrip.WagonTripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wagonTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WagonTripExists(wagonTrip.WagonTripId))
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
            ViewData["TrainTripId"] = new SelectList(_context.TrainTrips, "TrainTripId", "TrainTripId", wagonTrip.TrainTripId);
            ViewData["WagonId"] = new SelectList(_context.Wagons, "WagonId", "Name", wagonTrip.WagonId);
            return View(wagonTrip);
        }

        // GET: WagonTrips/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wagonTrip = await _context.WagonTrips
                .Include(w => w.TrainTrip)
                .Include(w => w.Wagon)
                .FirstOrDefaultAsync(m => m.WagonTripId == id);
            if (wagonTrip == null)
            {
                return NotFound();
            }

            return View(wagonTrip);
        }

        // POST: WagonTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var wagonTrip = await _context.WagonTrips.FindAsync(id);
            _context.WagonTrips.Remove(wagonTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WagonTripExists(Guid id)
        {
            return _context.WagonTrips.Any(e => e.WagonTripId == id);
        }
    }
}
