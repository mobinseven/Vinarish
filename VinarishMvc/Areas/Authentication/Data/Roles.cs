using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishMvc.Areas.Authentication.Data
{
    public class RolesList
    {
        public static class UserRoleManagement
        {
            public const string PageName = "UserRole";
            public const string RoleName = "UserRole";
            public const string Path = "/Authentication/UserManagement";
            public const string ControllerName = "UserRole";
        }
        public static class Trains
        {
            public const string PageName = "Trains and Wagons";
            public const string RoleName = "Trains";
            public const string Path = "/Vinarish/Trains";
            public const string ControllerName = "Vinarish";
            public const string ActionName = "Trains";
        }
    }
}
