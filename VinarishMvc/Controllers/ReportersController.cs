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
    public class ReportersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reporters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.Reporters.Include(r => r.Department).ToListAsync();
            //foreach(Reporter r in applicationDbContext)
            //{
            //    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == r.VinarishUserId);
            //    r.UserName = user.UserName;
            //}
            return View(applicationDbContext);
        }

        // GET: Reporters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporter = await _context.Reporters
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.ReporterId == id);
            if (reporter == null)
            {
                return NotFound();
            }

            return View(reporter);
        }

        //// GET: Reporters/Create
        //public IActionResult Create()
        //{
        //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
        //    return View();
        //}
        // GET: Reporters/Create/[vinarishUserId]
        public IActionResult Create(string id)
        {
            var reporter = _context.Reporters.Where(x => x.VinarishUserId == id).FirstOrDefault();
            if (reporter != null)
            {
                return RedirectToAction(nameof(Index));
            }
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = user.UserName;
            ViewBag.VinarishUserId = id;
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Reporters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reporter reporter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reporter);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "UserProfiles", new { area = "Authentication" });
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", reporter.DepartmentId);
            return View(reporter);
        }

        // GET: Reporters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporter = await _context.Reporters.FindAsync(id);
            if (reporter == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", reporter.DepartmentId);
            return View(reporter);
        }

        // POST: Reporters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reporter reporter)
        {
            if (id != reporter.ReporterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reporter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporterExists(reporter.ReporterId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", reporter.DepartmentId);
            return View(reporter);
        }

        // GET: Reporters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporter = await _context.Reporters
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.ReporterId == id);
            if (reporter == null)
            {
                return NotFound();
            }

            return View(reporter);
        }

        // POST: Reporters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reporter = await _context.Reporters.FindAsync(id);
            _context.Reporters.Remove(reporter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReporterExists(int id)
        {
            return _context.Reporters.Any(e => e.ReporterId == id);
        }
    }
}