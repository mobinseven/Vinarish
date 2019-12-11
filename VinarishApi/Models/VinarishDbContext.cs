using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VinarishApi.Models
{
    public partial class VinarishDbContext : DbContext
    {
        public VinarishDbContext(DbContextOptions<VinarishDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            VinarishOnModelCreating(modelBuilder);
        }
    }
}