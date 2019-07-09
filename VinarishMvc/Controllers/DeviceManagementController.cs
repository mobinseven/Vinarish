using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VinarishMvc.Areas.Authentication.Data;
using VinarishMvc.Data;

namespace VinarishMvc.Controllers
{
    [Authorize(Roles = RolesList.Devices.RoleName)]
    public class DeviceManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeviceManagement
        public ActionResult Index()
        {
            ViewBag.dataSource = _context.DevicePlaces.ToList();
            return View();
        }
    }
}