using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Areas.Authentication.Data;
using VinarishMvc.Areas.Authentication.Models;
using VinarishMvc.Data;
using VinarishMvc.Models.Syncfusion;

namespace VinarishMvc.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    [Authorize(Roles = RolesList.UserRoleManagement.RoleName)]
    public class UserProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Authentication/UserProfiles
        public ActionResult Index()
        {
            //ViewBag.UserProfiles = await _context.UserProfile.ToListAsync();
            //return View();
            ViewBag.dataSource = _context.UserProfile.ToList();
            return View();
        }

        //// GET: Authentication/UserProfiles/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userProfile = await _context.UserProfile
        //        .FirstOrDefaultAsync(m => m.UserProfileId == id);
        //    if (userProfile == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userProfile);
        //}

        //// GET: Authentication/UserProfiles/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Authentication/UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<UserProfile> payload)
        {
                _context.Add(payload.value);
                await _context.SaveChangesAsync();
            ViewBag.dataSource = await _context.UserProfile.ToListAsync();
            return Json(payload.value);
        }

        //// GET: Authentication/UserProfiles/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userProfile = await _context.UserProfile.FindAsync(id);
        //    if (userProfile == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userProfile);
        //}

        // POST: Authentication/UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("value")][FromBody]CrudViewModel<UserProfile> payload)
        {
            if (ModelState.IsValid)
            {
                _context.Update(payload.value);
                await _context.SaveChangesAsync();
                // TODO: DbUpdateConcurrencyException
                return RedirectToAction(nameof(Index));
            }
            return View(payload.value);
        }

        //// GET: Authentication/UserProfiles/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userProfile = await _context.UserProfile
        //        .FirstOrDefaultAsync(m => m.UserProfileId == id);
        //    if (userProfile == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userProfile);
        //}

        // POST: Authentication/UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("key")][FromBody]CrudViewModel<UserProfile> payload)
        {
            var userProfile = await _context.UserProfile.FindAsync(Convert.ToInt32(payload.key));
            _context.UserProfile.Remove(userProfile);
            await _context.SaveChangesAsync();
            var data = _context.UserProfile.ToList();
            return Json(data);
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfile.Any(e => e.UserProfileId == id);
        }
    }
}
