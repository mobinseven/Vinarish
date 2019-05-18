using System;
using System.Collections.Generic;
using System.Text;
using Vinarish.Models;
using System.Linq;

namespace Vinarish.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TCMMSContext context)
        {
            context.Database.EnsureCreated();

            //if (context.Station.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //var stations = new Station[]
            //{
            //    new Station { Name = "تهران" },
            //    new Station { Name = "مشهد" },
            //    new Station { Name = "کرج" },
            //    new Station { Name = "رشت" },
            //    new Station { Name = "زنجان" },
            //    new Station { Name = "اهواز" },
            //    new Station { Name = "قم" },
            //    new Station { Name = "قزوین" }
            //};
            //foreach (Station s in stations)
            //{
            //    context.Station.Add(s);
            //}
            //context.SaveChanges();
        }
    }
}
