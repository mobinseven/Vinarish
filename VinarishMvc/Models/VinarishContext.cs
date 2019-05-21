using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VinarishMvc.Models
{
    public partial class VinarishContext : DbContext
    {
        public VinarishContext()
        {
        }

        public VinarishContext(DbContextOptions<VinarishContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<StatCode> StatCode { get; set; }
        public virtual DbSet<Train> Train { get; set; }
        public virtual DbSet<Wagon> Wagon { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tcmms;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Department");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Persons_Department");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasIndex(e => e.AppendixReportId);

                entity.HasIndex(e => e.CatId);

                entity.HasIndex(e => e.CodeId);

                entity.HasIndex(e => e.PlaceId);

                entity.HasIndex(e => e.ReporterId);

                entity.HasIndex(e => e.WagonId);

                entity.HasOne(d => d.AppendixReport)
                    .WithMany(p => p.InverseAppendixReport)
                    .HasForeignKey(d => d.AppendixReportId)
                    .HasConstraintName("FK_Report_AppendixReport");

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

                entity.HasOne(d => d.Wagon)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.WagonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Wagon");
            });

            modelBuilder.Entity<StatCode>(entity =>
            {
                entity.HasIndex(e => e.CatId);

                entity.HasIndex(e => e.Code)
                    .IsUnique()
                    .HasFilter("([Code] IS NOT NULL)");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.StatCode)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Code_Cat");
            });

            modelBuilder.Entity<Train>(entity =>
            {
                entity.HasIndex(e => e.TrainId)
                    .IsUnique();

                entity.Property(e => e.TrainId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Wagon>(entity =>
            {
                entity.HasIndex(e => e.TrainId);

                entity.HasIndex(e => e.WagonId)
                    .IsUnique();

                entity.Property(e => e.WagonId).ValueGeneratedNever();

                entity.HasOne(d => d.Train)
                    .WithMany(p => p.Wagon)
                    .HasForeignKey(d => d.TrainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wagon_Train");
            });
        }
    }
}
