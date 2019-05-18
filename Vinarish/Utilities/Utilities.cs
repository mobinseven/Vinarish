using MD.PersianDateTime.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinarish
{
    static public class Utilities
    {
        static public string GetPErsianDateTimeNow()
        {
            StringBuilder now = new StringBuilder();
            var persianDateTime = new PersianDateTime(DateTime.Now)
            {
                EnglishNumber = false
            };
            now.AppendFormat("{0}, {1}/{2}/{3} {4}:{5}\n",
                          persianDateTime.GetLongDayOfWeekName,
                          persianDateTime.Month,
                          persianDateTime.Day,
                          persianDateTime.Year,
                          persianDateTime.Hour,
                          persianDateTime.Minute);
            return now.ToString();
        }
    }
}
