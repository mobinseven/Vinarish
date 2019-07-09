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
            var applicationDbContext = _context.TrainTrips
                //.Include(t => t.Reporter)
                .Include(t => t.Train)
                .Include(t => t.WagonsOfTrip)
                .ThenInclude(w => w.Wagon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TrainTrips/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTrip = await _context.TrainTrips
                //.Include(t => t.Reporter)
                .Include(t => t.Train)
                .Include(t => t.WagonsOfTrip)
                .ThenInclude(w => w.Wagon)
                .ThenInclude(w => w.Reports)
                .FirstOrDefaultAsync(m => m.TrainTripId == id);
            if (trainTrip == null)
            {
                return NotFound();
            }

            return View(trainTrip);
        }

        public class CreateViewModel
        {
            public TrainTrip TrainTrip { get; set; } = new TrainTrip { DateTime = Utilities.GetPErsianDateTimeNow() };
            public Dictionary<string, bool> Wagons { get; set; } = new Dictionary<string, bool>();
        }

        // GET: TrainTrips/Create
        public IActionResult Create()
        {
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName");
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "Name");
            CreateViewModel model = new CreateViewModel();
            List<Wagon> wagons = _context.Wagons.OrderBy(w => w.Name).ToList();
            foreach (Wagon w in wagons)
            {
                model.Wagons.Add(w.Name, false);
            }
            return View(model);
        }

        // GET: TrainTrips/CreateForTrain/[TrainId]
        public IActionResult CreateForTrain(int? id)
        {
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName");
            //ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "Name");
            CreateViewModel model = new CreateViewModel();
            model.TrainTrip.Train = _context.Trains.Find(id);
            List<Wagon> wagons = _context.Wagons.OrderBy(w => w.Name).ToList();
            foreach (Wagon w in wagons)
            {
                model.Wagons.Add(w.Name, false);
            }
            return View(model);
        }

        // POST: TrainTrips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            model.TrainTrip.TrainTripId = Guid.NewGuid();
            _context.Add(model.TrainTrip);
            List<WagonTrip> WagonTrips = new List<WagonTrip>();
            foreach (var item in model.Wagons)
            {
                if (item.Value)
                {
                    WagonTrips.Add(new WagonTrip
                    {
                        TrainTripId = model.TrainTrip.TrainTripId,
                        WagonId = _context.Wagons.Where(w => w.Name == item.Key).FirstOrDefault().WagonId
                    });
                }
            }
            _context.WagonTrips.AddRange(WagonTrips);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TrainTrips/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CreateViewModel model = new CreateViewModel();
            model.TrainTrip = await _context.TrainTrips.FindAsync(id);
            if (model.TrainTrip == null)
            {
                return NotFound();
            }

            List<Wagon> wagons = _context.Wagons.OrderBy(w => w.Name).ToList();
            foreach (Wagon w in wagons)
            {
                model.Wagons.Add(w.Name, false);
            }
            List<WagonTrip> IncludedWagons = _context.WagonTrips.Where(w => w.TrainTripId == id).Include(wt => wt.Wagon).ToList();
            foreach (WagonTrip w in IncludedWagons)
            {
                model.Wagons[w.Wagon.Name] = true;
            }
            ViewData["ReporterId"] = new SelectList(_context.Reporters, "ReporterId", "UserName");
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "Name");
            return View(model);
        }

        // POST: TrainTrips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }
            _context.Update(model.TrainTrip);
            _context.WagonTrips.RemoveRange(_context.WagonTrips.Where(w => w.TrainTripId == id).Include(wt => wt.Wagon).ToList());
            List<WagonTrip> WagonTrips = new List<WagonTrip>();
            foreach (var item in model.Wagons)
            {
                if (item.Value)
                {
                    WagonTrips.Add(new WagonTrip
                    {
                        TrainTripId = model.TrainTrip.TrainTripId,
                        WagonId = _context.Wagons.Where(w => w.Name == item.Key).FirstOrDefault().WagonId
                    });
                }
            }
            _context.WagonTrips.AddRange(WagonTrips);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: TrainTrips/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTrip = await _context.TrainTrips
                //.Include(t => t.Reporter)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trainTrip = await _context.TrainTrips.FindAsync(id);
            _context.TrainTrips.Remove(trainTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainTripExists(Guid id)
        {
            return _context.TrainTrips.Any(e => e.TrainTripId == id);
        }
    }
}