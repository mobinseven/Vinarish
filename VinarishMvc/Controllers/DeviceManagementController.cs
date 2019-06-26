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
        [HttpGet]
        public IActionResult Grid(Guid id)
        {
            var deviceType = _context.DeviceTypes
                .Where(dt => dt.DeviceTypeId == id)
                .Include(dt => dt.DevicePlaces)
                .FirstOrDefault();
            List<DevicePlace> data = deviceType.DevicePlaces.ToList();

            ViewBag.dataSource = data;
            return PartialView(deviceType);
        }

        //[HttpGet]
        //public IActionResult Grid()
        //{
        //    List<Department> data = _context.Departments.ToList();
        //    ViewBag.dataSource = data;
        //    return PartialView();
        //}
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<DevicePlace> payload)
        {
            _context.Add(payload.value);
            await _context.SaveChangesAsync();
            ViewBag.dataSource = await _context.DevicePlaces.ToListAsync();
            return Json(payload.value);
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind("value")][FromBody]CrudViewModel<Wagon> payload)
        {
            if (ModelState.IsValid)
            {
                _context.Update(payload.value);
                await _context.SaveChangesAsync();
            }
            return View(payload.value);
        }
        [HttpPost, ActionName("Remove")]
        public async Task<IActionResult> Remove([Bind("key")][FromBody]CrudViewModel<Wagon> payload)
        {
            var Wagon = await _context.Wagons.FindAsync(Convert.ToInt32(payload.key));
            _context.Wagons.Remove(Wagon);
            await _context.SaveChangesAsync();
            var data = _context.Wagons.ToList();
            return Json(data);
        }
        public class TreeGridData
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public string Code { get; set; }
            public string ParentItem { get; set; }
            public bool isParent { get; set; }
        }
        public IActionResult TreeGrid()
        {
            List<DeviceType> dts = _context.DeviceTypes.ToList();
            List<DevicePlace> dps = _context.DevicePlaces.ToList();
            List<TreeGridData> result = new List<TreeGridData>();
            foreach (DeviceType dt in dts)
            {
                result.Add(new TreeGridData()
                {
                    Id = dt.DeviceTypeId.ToString(),
                    Text = dt.Name,
                    Code = "L",
                    isParent = true,
                    ParentItem = null
                });
            }
            foreach (DevicePlace dp in dps)
            {
                result.Add(new TreeGridData()
                {
                    Id = dp.DevicePlaceId.ToString(),
                    Text = dp.Description,
                    Code = dp.Code,
                    ParentItem = dp.DeviceTypeId.ToString(),
                    isParent = false
                });
            }

            ViewBag.dataSource = result;
            return View();

        }
    }
}
