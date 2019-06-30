using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace VinarishMvc
{
    static public class Expressions
    {
        static public readonly string MalfunctionReportToolTip = "گزارش";
        static public readonly string RepairReportToolTip = "رفع شده";
        static public readonly string NotRepairedReportToolTip = "رفع نشده";
        static public readonly string InProgressToolTip = "در دست بررسی";
    }
    static public class Utilities
    {
        //static public string GetPErsianDateTimeNow()
        //{
        //    return ConvertToPersianDate(DateTime.Now);
        //}
        //static public string ConvertToPersianDate(DateTime dateTime)
        //{
        //    StringBuilder time = new StringBuilder();
        //    var persianDateTime = new PersianDateTime(dateTime)
        //    {
        //        EnglishNumber = false
        //    };
        //    time.AppendFormat("{0}, {1}/{2}/{3} {4}:{5}\n",
        //                  persianDateTime.GetLongDayOfWeekName,
        //                  persianDateTime.GetShortYear,
        //                  persianDateTime.Month,
        //                  persianDateTime.Day,
        //                  persianDateTime.Hour,
        //                  persianDateTime.Minute);
        //    return time.ToString();
        //}
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
        public static void LoadSearchViewBag(IEnumerable<VinarishMvc.Models.Report> reports, dynamic ViewData)
        {
            //ViewData["Cats"] = new SelectList(reports.Select(r => r.Device).Distinct(), "DeviceId", "CategoryName");
            //ViewData["Codes"] = new SelectList(reports.Select(r => r.Code).Distinct(), "Id", "FullName");
            //ViewData["Places"] = new SelectList(reports.Select(r => r.Place).Distinct(), "Id", "FullName");
            //ViewData["Reporters"] = new SelectList(reports.Select(r => r.Reporter).Distinct(), "Id", "FullName");
            //ViewData["Wagons"] = new SelectList(reports.Select(r => r.Wagon).Distinct(), "Id", "WagonId");
            //ViewData["Trains"] = new SelectList(reports.Select(r => r.Wagon.Train).Distinct(), "Id", "Train");
        }
    }
}
