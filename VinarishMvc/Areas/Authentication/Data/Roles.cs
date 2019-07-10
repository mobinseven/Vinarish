using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishMvc.Areas.Authentication.Data
{
    public class RolesList
    {
        public static class Admin
        {
            public const string RoleName = "Admin";
            public const string RoleDisplayName = Expressions.Admin;
        }

        public static class User
        {
            public const string RoleName = "User";
            public const string RoleDisplayName = Expressions.User;
        }
    }
}