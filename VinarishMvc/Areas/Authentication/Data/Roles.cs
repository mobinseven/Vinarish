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
            public const string RoleName = "UserRole";
            public const string RoleDisplayName = Expressions.Users;
        }

        public static class Trains
        {
            public const string RoleName = "Trains";
            public const string RoleDisplayName = Expressions.Trains;
        }

        public static class Trips
        {
            public const string RoleName = "Trips";
            public const string RoleDisplayName = Expressions.Trips;
        }

        public static class Devices
        {
            public const string RoleName = "Devices";
            public const string RoleDisplayName = Expressions.Devices;
        }

        public static class Wagons
        {
            public const string RoleName = "Wagons";
            public const string RoleDisplayName = Expressions.Wagons;
        }

        public static class Reports
        {
            public const string RoleName = "Reports";
            public const string RoleDisplayName = Expressions.Reports;
        }
    }
}