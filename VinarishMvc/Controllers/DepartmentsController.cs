using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Data;
using VinarishMvc.Models;
using VinarishMvc.Models.Syncfusion;

namespace VinarishMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Departments
        public ActionResult IndexSync()
        {
            //ViewBag.UserProfiles = await _context.UserProfile.ToListAsync();
            //return View();
            ViewBag.dataSource = _context.Departments.ToList();
            return View();
        }


        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<Department> payload)
        {
            _context.Add(payload.value);
            await _context.SaveChangesAsync();
            ViewBag.dataSource = await _context.Departments.ToListAsync();
            return Json(payload.value);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("value")][FromBody]CrudViewModel<Department> payload)
        {
            if (ModelState.IsValid)
            {
                _context.Update(payload.value);
                await _context.SaveChangesAsync();
                // TODO: DbUpdateConcurrencyException
                return RedirectToAction(nameof(IndexSync));
            }
            return View(payload.value);
        }
        // POST: Departments/Remove/5
        [HttpPost, ActionName("Remove")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([Bind("key")][FromBody]CrudViewModel<Department> payload)
        {
            var Departments = await _context.Departments.FindAsync(Convert.ToInt32(payload.key));
            _context.Departments.Remove(Departments);
            await _context.SaveChangesAsync();
            var data = _context.Departments.ToList();
            return Json(data);
        }
    }
}
