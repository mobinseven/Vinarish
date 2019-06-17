using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinarishMvc.Areas.Identity.Models;
using VinarishMvc.Areas.Authentication.Data;
namespace VinarishMvc.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    [Authorize(Roles = RolesList.UserRoleManagement.RoleName)]
    public class UserRoleController : Controller
    {
        private readonly UserManager<VinarishUser> _userManager;

        public UserRoleController(UserManager<VinarishUser> userManager)
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Role()
        {
            return View();
        }

        public IActionResult ChangeRole()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            VinarishUser user = await _userManager.GetUserAsync(User);
            return View(user);
        }
    }
}