using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using VinarishMvc.Areas.Authentication.Data;
using VinarishMvc.Areas.Authentication.Models;
using VinarishMvc.Areas.Authentication.Services;
using VinarishMvc.Areas.Identity.Models;
using VinarishMvc.Data;

namespace VinarishMvc.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    [Authorize(Roles = RolesList.Admin.RoleName)]
    public class UserProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<VinarishUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoles _roles;

        public UserProfilesController(ApplicationDbContext context,
                        UserManager<VinarishUser> userManager,
                        RoleManager<IdentityRole> roleManager,
                        IRoles roles)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _roles = roles;
        }

        // GET: Authentication/UserProfiles
        public async Task<IActionResult> Index()
        {
            ViewBag.Reporters = _context.Reporters.ToList();
            return View(await _context.UserProfile.ToListAsync());
        }

        // GET: Authentication/UserProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.UserProfileId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: Authentication/UserProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authentication/UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserProfileId,FirstName,LastName,Email,Password,ConfirmPassword,OldPassword,ProfilePicture,VinarishUserId")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                UserProfile register = userProfile;
                if (register.Password.Equals(register.ConfirmPassword))
                {
                    var username = register.Email;
                    register.Email = register.Email + "@vinarish.com";
                    VinarishUser user = new VinarishUser() { Email = register.Email, UserName = username, EmailConfirmed = true };
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        register.Password = user.PasswordHash;
                        register.ConfirmPassword = user.PasswordHash;
                        register.VinarishUserId = user.Id;
                        _context.UserProfile.Add(register);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userProfile);
        }

        // GET: Authentication/UserProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return View(userProfile);
        }

        // POST: Authentication/UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserProfileId,FirstName,LastName,Email,Password,ConfirmPassword,OldPassword,ProfilePicture,VinarishUserId")] UserProfile userProfile)
        {
            if (id != userProfile.UserProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.UserProfileId))
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
            return View(userProfile);
        }

        // GET: Authentication/UserProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.UserProfileId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: Authentication/UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfile.FindAsync(id);
            if (userProfile != null)
            {
                var user = _context.Users.Where(x => x.Id.Equals(userProfile.VinarishUserId)).FirstOrDefault();
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _context.Remove(userProfile);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfile.Any(e => e.UserProfileId == id);
        }

        public class UserRoleViewModel
        {
            public string VinarishUserId { get; set; }
            public Dictionary<string, bool> RoleList { get; set; } = new Dictionary<string, bool>();
            public UserProfile Profile { get; set; }
        }

        // GET: Authentication/UserProfiles/ChangeRole/5
        public async Task<IActionResult> ChangeRole(int? id)
        {
            var userProfile = await _context.UserProfile
               .FirstOrDefaultAsync(m => m.UserProfileId == id);
            await _roles.GenerateRolesFromPagesAsync();
            var user = await _userManager.FindByIdAsync(userProfile.VinarishUserId);
            var roles = _roleManager.Roles.ToList();
            UserRoleViewModel Item = new UserRoleViewModel
            {
                Profile = userProfile,
                VinarishUserId = userProfile.VinarishUserId
            };
            foreach (var role in roles)
            {
                bool isInRole = (await _userManager.IsInRoleAsync(user, role.Name)) ? true : false;
                Item.RoleList.Add(role.Name, isInRole);
            }
            return View(Item);
        }

        // POST: Authentication/UserProfiles/ChangeRole/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(int id, [Bind("VinarishUserId,RoleList,Profile")] UserRoleViewModel UserRoleViewModel)
        {
            var userProfile = await _context.UserProfile
               .FirstOrDefaultAsync(m => m.UserProfileId == id);
            var user = await _userManager.FindByIdAsync(userProfile.VinarishUserId);
            if (user != null)
            {
                foreach (var item in UserRoleViewModel.RoleList)
                {
                    if (item.Value)
                    {
                        await _userManager.AddToRoleAsync(user, item.Key);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, item.Key);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userProfile);
        }

        // GET: Authentication/UserProfiles/ChangePassword/5
        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            userProfile.OldPassword = userProfile.Password;
            userProfile.Password = null;
            userProfile.ConfirmPassword = null;
            return View(userProfile);
        }

        // POST: Authentication/UserProfiles/ChangePassword/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, [Bind("UserProfileId,FirstName,LastName,Email,Password,ConfirmPassword,OldPassword,ProfilePicture,VinarishUserId")] UserProfile userProfile)
        {
            if (id != userProfile.UserProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (userProfile.Password.Equals(userProfile.ConfirmPassword))
                    {
                        var user = await _userManager.FindByIdAsync(userProfile.VinarishUserId);
                        await _userManager.RemovePasswordAsync(user);
                        var result = await _userManager.AddPasswordAsync(user, userProfile.Password);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.UserProfileId))
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
            return View(userProfile);
        }

        // GET: Authentication/UserProfiles/Upload
        public IActionResult Upload()
        {
            return View();
        }
        // POST: Authentication/UserProfiles/Upload
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(5000000)]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            IFormFile file = File;
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            List<UserProfile> UserProfiles = new List<UserProfile>();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;


                    for (int i = 1; i < totalRows; i++)
                    {
                        var name = ((object[,])(worksheet.Cells.Value))[i, 0].ToString();
                        UserProfile register = new UserProfile
                        {
                            Email = name,
                            Password = "123456",
                            ConfirmPassword = "123456"
                        };
                        if (register.Password.Equals(register.ConfirmPassword))
                        {
                            var username = register.Email;
                            register.Email = register.Email + "@vinarish.com";
                            VinarishUser user = new VinarishUser() { Email = register.Email, UserName = username, EmailConfirmed = true };
                            var result = await _userManager.CreateAsync(user, register.Password);
                            if (result.Succeeded)
                            {
                                register.Password = user.PasswordHash;
                                register.ConfirmPassword = user.PasswordHash;
                                register.VinarishUserId = user.Id;
                                _context.UserProfile.Add(register);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}