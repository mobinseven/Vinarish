using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishMvc.Areas.Authentication.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public int CounterId { get; set; }
        public string VinarishUserId { get; set; }
        public string RoleName { get; set; }
        public bool IsHaveAccess { get; set; }
    }
}
