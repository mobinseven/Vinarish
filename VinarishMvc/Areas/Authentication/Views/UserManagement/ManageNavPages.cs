using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace VinarishMvc.Areas.Authentication.Views.UserManagement
{
    public class ManageNavPages
    {
        public static string UsersIndex => "Index";
        public static string RolesIndex => "RolesIndex";
        public static string CreateNewUser => "CreateNewUser";
        public static string UsersIndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, UsersIndex);
        public static string RolesIndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, RolesIndex);
        public static string CreateNewUserNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateNewUser);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
