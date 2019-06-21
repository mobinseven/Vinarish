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
    public class TrainTripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainTripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainTrips
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TrainTrips.Include(t => t.Reporter).Include(t => t.Train);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TrainTrips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTrip = await _context.TrainTrips
                .Include(t => t.Reporter)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.TrainTripId == id);
            if (trainTrip == null)
            {
                return NotFound();
            }

            return View(trainTrip);
        }

        // GET: TrainTrips/Create
        public IActionResult Create()
        {
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId");
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "Name");
            return View();
        }

        // POST: TrainTrips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainTripId,DateTime,TrainId,ReporterId")] TrainTrip trainTrip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId", trainTrip.ReporterId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "Name", trainTrip.TrainId);
            return View(trainTrip);
        }

        // GET: TrainTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTrip = await _context.TrainTrips.FindAsync(id);
            if (trainTrip == null)
            {
                return NotFound();
            }
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId", trainTrip.ReporterId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "Name", trainTrip.TrainId);
            return View(trainTrip);
        }

        // POST: TrainTrips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainTripId,DateTime,TrainId,ReporterId")] TrainTrip trainTrip)
        {
            if (id != trainTrip.TrainTripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainTripExists(trainTrip.TrainTripId))
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
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "VinarishUserId", trainTrip.ReporterId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "Name", trainTrip.TrainId);
            return View(trainTrip);
        }

        // GET: TrainTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTrip = await _context.TrainTrips
                .Include(t => t.Reporter)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.TrainTripId == id);
            if (trainTrip == null)
            {
                return NotFound();
            }

            return View(trainTrip);
        }

        // POST: TrainTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainTrip = await _context.TrainTrips.FindAsync(id);
            _context.TrainTrips.Remove(trainTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainTripExists(int id)
        {
            return _context.TrainTrips.Any(e => e.TrainTripId == id);
        }
    }
}
