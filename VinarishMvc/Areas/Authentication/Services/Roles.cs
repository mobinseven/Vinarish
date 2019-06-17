using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinarishMvc.Areas.Authentication.Data;
using VinarishMvc.Areas.Identity.Models;

namespace VinarishMvc.Areas.Authentication.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string VinarishUserId);
    }
    public class Roles : IRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<VinarishUser> _userManager;

        public Roles(RoleManager<IdentityRole> roleManager,
            UserManager<VinarishUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task GenerateRolesFromPagesAsync()
        {
            Type t = typeof(RolesList);
            foreach (Type item in t.GetNestedTypes())
            {
                foreach (var itm in item.GetFields())
                {
                    if (itm.Name.Contains("RoleName"))
                    {
                        string roleName = (string)itm.GetValue(item);
                        if (!await _roleManager.RoleExistsAsync(roleName))
                            await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
        }

        public async Task AddToRoles(string VinarishUserId)
        {
            var user = await _userManager.FindByIdAsync(VinarishUserId);
            if (user != null)
            {
                var roles = _roleManager.Roles;
                List<string> listRoles = new List<string>();
                foreach (var item in roles)
                {
                    listRoles.Add(item.Name);
                }
                await _userManager.AddToRolesAsync(user, listRoles);
            }
        }
    }
}
