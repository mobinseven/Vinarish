using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Areas.Authentication.Models;
using VinarishMvc.Areas.Identity.Models;
using VinarishMvc.Models;

namespace VinarishMvc.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<VinarishUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<VinarishUser> VinarishUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            VinarishOnModelCreating(modelBuilder);
        }
    }
}
