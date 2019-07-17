using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VinarishMvc.Areas.Authentication.Models;
using VinarishMvc.Areas.Identity.Models;

namespace VinarishMvc.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<VinarishUser>, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<VinarishUser> VinarishUser { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            VinarishOnModelCreating(modelBuilder);
        }
    }
}
