using MD.PersianDateTime.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        static public string ConvertToPersianDate(DateTime dateTime)
        {
            StringBuilder time = new StringBuilder();
            var persianDateTime = new PersianDateTime(dateTime)
            {
                EnglishNumber = false
            };
            time.AppendFormat("{0}, {1}/{2}/{3} {4}:{5}\n",
                          persianDateTime.GetLongDayOfWeekName,
                          persianDateTime.Month,
                          persianDateTime.Day,
                          persianDateTime.GetShortYear,
                          persianDateTime.Hour,
                          persianDateTime.Minute);
            return DigitsToPersian(time.ToString());
        }
        private static readonly CultureInfo persian = new CultureInfo("fa-IR");
        private static readonly CultureInfo latin = new CultureInfo("en-US");

        public static string DigitsToPersian(string input)
        {
            var PersianDigits = persian.NumberFormat.NativeDigits;
            for (int i = 0; i < PersianDigits.Length; i++)
            {
                input = input.Replace(i.ToString(), PersianDigits[i]);
            }
            return input;
        }
    }
}
