using Microsoft.EntityFrameworkCore;
using Vinarish.Models;
namespace Vinarish.Data
{
    public partial class TCMMSContext : DbContext
    {
        public TCMMSContext()
        {
        }

        public TCMMSContext(DbContextOptions<TCMMSContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Train> Train { get; set; }
        public virtual DbSet<Wagon> Wagon { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<StatCode> StatCode { get; set; }
        public virtual DbSet<Report> Report { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=185.10.75.8;User ID=vinarish;Password=Hibernate70!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(true);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId);

                entity.Property(e => e.FirstName).IsUnicode(true);

                entity.Property(e => e.LastName).IsUnicode(true);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Persons_Department");
            });

            modelBuilder.Entity<Train>(entity =>
            {
                entity.HasIndex(e => e.TrainId).IsUnique(true);

                entity.Property(e => e.TrainId).ValueGeneratedNever();

                entity.HasIndex(e => e.HeadId);

                entity.HasIndex(e => e.OfficerId);

                entity.HasOne(d => d.Head)
                    .WithMany(p => p.TrainHead)
                    .HasForeignKey(d => d.HeadId)
                    .HasConstraintName("FK_Trains_Head");

                entity.HasOne(d => d.Officer)
                    .WithMany(p => p.TrainOfficer)
                    .HasForeignKey(d => d.OfficerId)
                    .HasConstraintName("FK_Trains_ElecOfficer");
            });

            modelBuilder.Entity<Wagon>(entity =>
            {
                entity.HasIndex(e => e.TrainId);

                entity.HasIndex(e => e.WagonId).IsUnique(true);

                entity.Property(e => e.WagonId).ValueGeneratedNever();

                entity.HasOne(d => d.Train)
                    .WithMany(p => p.Wagon)
                    .HasForeignKey(d => d.TrainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wagon_Train");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.OrderNumber)
                    .ValueGeneratedNever().IsRequired(false);

                entity.HasIndex(e => new { e.OrderNumber })
                    .IsUnique(true);
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Department");
            });

            modelBuilder.Entity<StatCode>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique(true);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.StatCode)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Code_Cat");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.AppendixReportId).HasDefaultValue(null);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Cat");

                entity.HasOne(d => d.Code)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.CodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_CodeId");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_PlaceId");

                entity.HasOne(d => d.Reporter)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Reporter");

                entity.HasOne(d => d.AppendixReport)
                    .WithMany(p => p.AppendixReports)
                    .HasForeignKey(d => d.AppendixReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_AppendixReport");

                entity.HasOne(d => d.Wagon)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.WagonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Wagon");
            });

        }

        public DbSet<Vinarish.Models.Place> Place { get; set; }
    }
}
