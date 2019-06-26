using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Data;
using VinarishMvc.Models;
using VinarishMvc.Models.Syncfusion;

namespace VinarishMvc.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DevicePlacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DevicePlacesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class TreeGridData
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public string Code { get; set; }
            public string ParentItem { get; set; }
            public bool isParent { get; set; }
        }
        // GET: api/DevicePlaces
        [HttpGet]
        public ActionResult<IEnumerable<DevicePlace>> GetDevicePlaces()
        {
            List<DeviceType> dts = _context.DeviceTypes.ToList();
            List<DevicePlace> dps = _context.DevicePlaces.ToList();
            List<TreeGridData> Items = new List<TreeGridData>();
            foreach (DeviceType dt in dts)
            {
                Items.Add(new TreeGridData()
                {
                    Id = dt.DeviceTypeId.ToString(),
                    Text = dt.Name,
                    Code = "",
                    isParent = true,
                    ParentItem = null
                });
            }
            foreach (DevicePlace dp in dps)
            {
                Items.Add(new TreeGridData()
                {
                    Id = dp.DevicePlaceId.ToString(),
                    Text = dp.Description,
                    Code = dp.Code,
                    ParentItem = dp.DeviceTypeId.ToString(),
                    isParent = false
                });
            }

            int Count = Items.Count();
            return Ok(new { Items, Count });
        }



        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<TreeGridData> payload)
        {
            //Currency currency = payload.value;
            //_context.Currency.Add(currency);
            //_context.SaveChanges();
            return Ok(payload.value);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<TreeGridData> payload)
        {
            //Currency currency = payload.value;
            //_context.Currency.Update(currency);
            //_context.SaveChanges();
            return Ok(payload.value);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<TreeGridData> payload)
        {
            //Currency currency = _context.Currency
            //    .Where(x => x.CurrencyId == (int)payload.key)
            //    .FirstOrDefault();
            //_context.Currency.Remove(currency);
            //_context.SaveChanges();
            return Ok(payload.value);

        }
    }
}
