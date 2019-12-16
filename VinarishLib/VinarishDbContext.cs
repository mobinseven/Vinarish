using Microsoft.EntityFrameworkCore;

namespace VinarishLib.Models
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