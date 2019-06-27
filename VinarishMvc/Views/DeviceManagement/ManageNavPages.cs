using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VinarishMvc.DeviceManagement
{
    public static class ManageNavPages
    {
        public static string DevicePlaces => "DevicePlaces";

        public static string DeviceTypes => "DeviceTypes";

        public static string DeviceStatus => "DeviceStatus";

        public static string DevicePlacesNavClass(ViewContext viewContext) => PageNavClass(viewContext, DevicePlaces);

        public static string DeviceTypesNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeviceTypes);

        public static string DeviceStatusNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeviceStatus);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(viewContext.HttpContext.Request);
            return activePage.Contains( page) ? "active" : null;
        }
    }
}