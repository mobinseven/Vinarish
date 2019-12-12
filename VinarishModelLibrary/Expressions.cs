using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VinarishApi
{
    static public class Expressions
    {
        public const string AccessesOf = "دسترسی‌های ";
        public const string Or = "یا ";
        public const string LogOut = "برون رفت از سامانه ";
        public const string LogOutMessage = "شما از سامانه بیرون شدید. ";
        public const string Admin = "مدیر سامانه  ";
        public const string BackToIndex = "بازگشت به فهرست";
        public const string ChildReports = "گزارشهای فرزند";
        public const string Code = "کد ";
        public const string DateTime = "زمان ";
        public const string DateTimeModified = "واپسین ویرایش";
        public const string Delete = "پاک شود";
        public const string ToDelete = "پاک کردن ";
        public const string Department = "بخش ";
        public const string Departments = "بخش‌ها ";
        public const string DevicePlaces = "موقعیت تجهیز ";
        public const string DeviceStatus = "کد راهنما ";
        public const string DeviceStatusMalfunction = "کد خرابی ";
        public const string DeviceStatusNRepair = "کد عدم رفع خرابی ";
        public const string DeviceStatusRepair = "کد رفع خرابی ";
        public const string DeviceTypes = "نوع تجهیز ";
        public const string InProgressToolTip = "در دست بررسی ";
        public const string MalfunctionReportToolTip = "گزارش ";
        public const string NotRepairedReportToolTip = "رفع نشده ";
        public const string Options = "گزینه‌ها";
        public const string ParentReport = "گزارش پدر";
        public const string RepairReportToolTip = "رفع شده ";
        public const string Report = "گزارش ";
        public const string Reporter = "گزارشگر ";
        public const string Reporters = "گزارشگران ";
        public const string Assistants = "همکاران ";
        public const string Assistant = "همکار ";
        public const string AsAssistant = "در جایگاه همکار ";
        public const string Reports = "گزارش‌ها ";
        public const string Site = "سایت";
        public const string Sites = "سایت ها";
        public const string Edit = "ویرایش ";
        public const string Add = "افزودن ";
        public const string Status = "وضعیت ";
        public const string Train = "رام ";
        public const string Trains = "رام‌ها";
        public const string Trips = "سفرها";
        public const string Trip = "سفر ";
        public const string Type = "نوع ";
        public const string UserName = "نام کاربری";
        public const string Wagon = "واگن";
        public const string Wagons = "واگن‌ها";
        public const string Count = "شمار";
        public const string WagonNumber = "شماره واگن";
        public const string Details = "جزئیات ";
        public const string Devices = "تجهیزات ";
        public const string ImportFromExcel = "افزودن از اکسل";
        public const string LostReports = "گزارشهای نادرست";
        public const string Repairer = "تعمیرکننده";
        public const string Repair = "تعمیر";
        public const string RepairNot = "عدم تعمیر";
        public const string Index = "فهرست ";
        public const string WaitingReports = "گزارش‌ها ";
        public const string ProcessedReports = "خرابی‌های رفع شده ";
        public const string Users = "کاربران ";
        public const string User = "کاربر ";
        public const string Malfunction = "خرابی ";

        public static string IndexTitle(string item)
        {
            return Index + item;
        }

        public static string DeleteItemTitle(string item)
        {
            return ToDelete + item;
        }

        public static string ImportFromExcelTitle(string item)
        {
            return "افزودن " + item + "  از اکسل";
        }

        public static string DeleteItemAsk(string item)
        {
            return "آیا میخواهید این " + item + " پاک شود؟";
        }

        public static string EditItemTitle(string item)
        {
            return Edit + item;
        }

        public static string AddItemTitle(string item)
        {
            return Add + item;
        }
    }
}