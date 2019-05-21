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
            return ConvertToPersianDate(DateTime.Now);
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
                          persianDateTime.GetShortYear,
                          persianDateTime.Month,
                          persianDateTime.Day,
                          persianDateTime.Hour,
                          persianDateTime.Minute);
            return DigitsToPersian(time.ToString());
        }
        private static readonly CultureInfo persian = new CultureInfo("fa-IR");

        public static string DigitsToPersian(string input)
        {
            var PersianDigits = persian.NumberFormat.NativeDigits;
            for (int i = 0; i < PersianDigits.Length; i++)
            {
                input = input.Replace(i.ToString(), PersianDigits[i]);
            }
            return input;
        }

        public static string PadNumbers(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }
    }
}
