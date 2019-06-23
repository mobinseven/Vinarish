using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Syncfusion.EJ2;
using Syncfusion.EJ2.Navigations;
using VinarishMvc.Data;
using VinarishMvc.Models;
using VinarishMvc.Models.Syncfusion;

namespace VinarishMvc.Controllers
{
    public class DeviceManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceManagementController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class SubmitModel
        {
            public string Name { get; set; }
            public string PrimaryKey { get; set; }
            public string Value { get; set; }
        }
        public SubmitModel UpdateData([FromBody]SubmitModel payload)
        {
            var devicePlace = _context.DevicePlaces.Find(Guid.Parse(payload.PrimaryKey));
            if (payload.Name == "Description")
            {
                devicePlace.Description = payload.Value;
            }
            if (payload.Name == "Code")
            {
                devicePlace.Code = payload.Value;

            }
            _context.Update(devicePlace);
            _context.SaveChanges();
            return payload;
        }
        // GET: DeviceManagement
        public IActionResult Index()
        {
            ViewBag.Data =
                _context.Departments
                .Include(d => d.DeviceTypes)
                .ThenInclude(dt => dt.DevicePlaces)
                .Include(d => d.DeviceTypes)
                .ThenInclude(dt => dt.DeviceStatus)
                .ToList();

            return View();

        }
        public IActionResult About(Guid id)
        {
            var deviceType = _context.DeviceTypes.Where(dt => dt.DeviceTypeId == id)
                .FirstOrDefault();
            return PartialView(deviceType);
        }
    }
}
