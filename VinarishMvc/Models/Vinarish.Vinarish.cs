﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/28/2019 4:40:51 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using VinarishMvc.Areas.Identity.Models;
using VinarishMvc.Models;

namespace VinarishMvc.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<VinarishUser>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<Train> Trains
        {
            get;
            set;
        }

        public virtual DbSet<Wagon> Wagons
        {
            get;
            set;
        }

        public virtual DbSet<DeviceType> DeviceTypes
        {
            get;
            set;
        }

        public virtual DbSet<Report> Reports
        {
            get;
            set;
        }

        public virtual DbSet<Reporter> Reporters
        {
            get;
            set;
        }

        public virtual DbSet<DeviceStatus> DeviceStatus
        {
            get;
            set;
        }

        public virtual DbSet<WagonTrip> WagonTrips
        {
            get;
            set;
        }

        public virtual DbSet<Department> Departments
        {
            get;
            set;
        }

        public virtual DbSet<TrainTrip> TrainTrips
        {
            get;
            set;
        }

        public virtual DbSet<DevicePlace> DevicePlaces
        {
            get;
            set;
        }

        protected void VinarishOnModelCreating(ModelBuilder modelBuilder)
        {
            TrainMapping(modelBuilder);
            CustomizeTrainMapping(modelBuilder);

            WagonMapping(modelBuilder);
            CustomizeWagonMapping(modelBuilder);

            DeviceTypeMapping(modelBuilder);
            CustomizeDeviceTypeMapping(modelBuilder);

            ReportMapping(modelBuilder);
            CustomizeReportMapping(modelBuilder);

            ReporterMapping(modelBuilder);
            CustomizeReporterMapping(modelBuilder);

            DeviceStatusMapping(modelBuilder);
            CustomizeDeviceStatusMapping(modelBuilder);

            WagonTripMapping(modelBuilder);
            CustomizeWagonTripMapping(modelBuilder);

            DepartmentMapping(modelBuilder);
            CustomizeDepartmentMapping(modelBuilder);

            TrainTripMapping(modelBuilder);
            CustomizeTrainTripMapping(modelBuilder);

            DevicePlaceMapping(modelBuilder);
            CustomizeDevicePlaceMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }

        #region Train Mapping

        private void TrainMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>().ToTable(@"Trains");
            modelBuilder.Entity<Train>().Property<int>(x => x.TrainId).HasColumnName(@"TrainId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Train>().Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Train>().HasKey(@"TrainId");
            modelBuilder.Entity<Train>().HasIndex(@"Name").IsUnique(true);
        }

        partial void CustomizeTrainMapping(ModelBuilder modelBuilder);

        #endregion

        #region Wagon Mapping

        private void WagonMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wagon>().ToTable(@"Wagons");
            modelBuilder.Entity<Wagon>().Property<System.Guid>(x => x.WagonId).HasColumnName(@"WagonId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Wagon>().Property<int>(x => x.Number).HasColumnName(@"Number").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Wagon>().Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Wagon>().HasKey(@"WagonId");
            modelBuilder.Entity<Wagon>().HasIndex(@"Number").IsUnique(true);
            modelBuilder.Entity<Wagon>().HasIndex(@"Name").IsUnique(true);
        }

        partial void CustomizeWagonMapping(ModelBuilder modelBuilder);

        #endregion

        #region DeviceType Mapping

        private void DeviceTypeMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceType>().ToTable(@"DeviceTypes");
            modelBuilder.Entity<DeviceType>().Property<System.Guid>(x => x.DeviceTypeId).HasColumnName(@"DeviceTypeId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<DeviceType>().Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<DeviceType>().Property<System.Guid>(x => x.DepartmentId).HasColumnName(@"DepartmentId").ValueGeneratedNever();
            modelBuilder.Entity<DeviceType>().HasKey(@"DeviceTypeId");
            modelBuilder.Entity<DeviceType>().HasIndex(@"Name").IsUnique(true);
        }

        partial void CustomizeDeviceTypeMapping(ModelBuilder modelBuilder);

        #endregion

        #region Report Mapping

        private void ReportMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable(@"Reports");
            modelBuilder.Entity<Report>().Property<int>(x => x.ReportId).HasColumnName(@"ReportId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Report>().Property<System.DateTime>(x => x.DateTimeCreated).HasColumnName(@"DateTimeCreated").IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql(@"CURRENT_TIMESTAMP");
            modelBuilder.Entity<Report>().Property<System.DateTime>(x => x.DateTimeModified).HasColumnName(@"DateTimeModified").IsRequired().ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(@"CURRENT_TIMESTAMP");
            modelBuilder.Entity<Report>().Property<int>(x => x.ReporterId).HasColumnName(@"ReporterId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<VinarishMvc.Models.ReportStatus>(x => x.Status).HasColumnName(@"Status").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid>(x => x.DeviceStatusId).HasColumnName(@"DeviceStatusId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<int?>(x => x.AppendixReportId).HasColumnName(@"AppendixReportId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid>(x => x.DevicePlaceId).HasColumnName(@"DevicePlaceId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid>(x => x.WagonId).HasColumnName(@"WagonId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid?>(x => x.WagonTripId).HasColumnName(@"WagonTripId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().HasKey(@"ReportId");
        }

        partial void CustomizeReportMapping(ModelBuilder modelBuilder);

        #endregion

        #region Reporter Mapping

        private void ReporterMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reporter>().ToTable(@"Reporters");
            modelBuilder.Entity<Reporter>().Property<int>(x => x.ReporterId).HasColumnName(@"ReporterId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Reporter>().Property<string>(x => x.VinarishUserId).HasColumnName(@"VinarishUserId").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Reporter>().Property<System.Guid>(x => x.DepartmentId).HasColumnName(@"DepartmentId").ValueGeneratedNever();
            modelBuilder.Entity<Reporter>().Property<string>(x => x.UserName).HasColumnName(@"UserName").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Reporter>().HasKey(@"ReporterId");
            modelBuilder.Entity<Reporter>().HasIndex(@"VinarishUserId").IsUnique(true);
        }

        partial void CustomizeReporterMapping(ModelBuilder modelBuilder);

        #endregion

        #region DeviceStatus Mapping

        private void DeviceStatusMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceStatus>().ToTable(@"DeviceStatus");
            modelBuilder.Entity<DeviceStatus>().Property<System.Guid>(x => x.StatusId).HasColumnName(@"StatusId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<DeviceStatus>().Property<string>(x => x.Code).HasColumnName(@"Code").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().Property<string>(x => x.Text).HasColumnName(@"Text").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().Property<VinarishMvc.Models.DeviceStatusType?>(x => x.DeviceStatusType).HasColumnName(@"DeviceStatusType").ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().Property<System.Guid>(x => x.DeviceTypeId).HasColumnName(@"DeviceTypeId").ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().HasKey(@"StatusId");
            modelBuilder.Entity<DeviceStatus>().HasIndex(@"Code").IsUnique(true);
            modelBuilder.Entity<DeviceStatus>().HasIndex(@"Text").IsUnique(true);
        }

        partial void CustomizeDeviceStatusMapping(ModelBuilder modelBuilder);

        #endregion

        #region WagonTrip Mapping

        private void WagonTripMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WagonTrip>().ToTable(@"WagonTrips");
            modelBuilder.Entity<WagonTrip>().Property<System.Guid>(x => x.WagonTripId).HasColumnName(@"WagonTripId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<WagonTrip>().Property<System.Guid>(x => x.WagonId).HasColumnName(@"WagonId").ValueGeneratedNever();
            modelBuilder.Entity<WagonTrip>().Property<System.Guid>(x => x.TrainTripId).HasColumnName(@"TrainTripId").ValueGeneratedNever();
            modelBuilder.Entity<WagonTrip>().HasKey(@"WagonTripId");
        }

        partial void CustomizeWagonTripMapping(ModelBuilder modelBuilder);

        #endregion

        #region Department Mapping

        private void DepartmentMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable(@"Departments");
            modelBuilder.Entity<Department>().Property<System.Guid>(x => x.DepartmentId).HasColumnName(@"DepartmentId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Department>().HasKey(@"DepartmentId");
            modelBuilder.Entity<Department>().HasIndex(@"Name").IsUnique(true);
        }

        partial void CustomizeDepartmentMapping(ModelBuilder modelBuilder);

        #endregion

        #region TrainTrip Mapping

        private void TrainTripMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainTrip>().ToTable(@"TrainTrips");
            modelBuilder.Entity<TrainTrip>().Property<System.Guid>(x => x.TrainTripId).HasColumnName(@"TrainTripId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<TrainTrip>().Property<System.DateTime>(x => x.DateTime).HasColumnName(@"DateTime").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<TrainTrip>().Property<int>(x => x.TrainId).HasColumnName(@"TrainId").ValueGeneratedNever();
            modelBuilder.Entity<TrainTrip>().Property<int>(x => x.ReporterId).HasColumnName(@"ReporterId").ValueGeneratedNever();
            modelBuilder.Entity<TrainTrip>().HasKey(@"TrainTripId");
        }

        partial void CustomizeTrainTripMapping(ModelBuilder modelBuilder);

        #endregion

        #region DevicePlace Mapping

        private void DevicePlaceMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevicePlace>().ToTable(@"DevicePlaces");
            modelBuilder.Entity<DevicePlace>().Property<System.Guid>(x => x.DevicePlaceId).HasColumnName(@"DevicePlaceId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<DevicePlace>().Property<System.Guid>(x => x.DeviceTypeId).HasColumnName(@"DeviceTypeId").IsConcurrencyToken().ValueGeneratedNever();
            modelBuilder.Entity<DevicePlace>().Property<string>(x => x.Code).HasColumnName(@"Code").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<DevicePlace>().Property<string>(x => x.Description).HasColumnName(@"Description").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<DevicePlace>().HasKey(@"DevicePlaceId");
            modelBuilder.Entity<DevicePlace>().HasIndex(@"Code").IsUnique(true);
        }

        partial void CustomizeDevicePlaceMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {

            #region Train Navigation properties

            modelBuilder.Entity<Train>().HasMany(x => x.TrainTrips).WithOne(op => op.Train).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainId");

            #endregion

            #region Wagon Navigation properties

            modelBuilder.Entity<Wagon>().HasMany(x => x.Trips).WithOne(op => op.Wagon).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"WagonId");
            modelBuilder.Entity<Wagon>().HasMany(x => x.Reports).WithOne(op => op.Wagon).IsRequired(true).HasForeignKey(@"WagonId");

            #endregion

            #region DeviceType Navigation properties

            modelBuilder.Entity<DeviceType>().HasOne(x => x.Department).WithMany(op => op.DeviceTypes).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");
            modelBuilder.Entity<DeviceType>().HasMany(x => x.DevicePlaces).WithOne(op => op.DeviceType).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceTypeId");
            modelBuilder.Entity<DeviceType>().HasMany(x => x.DeviceStatus).WithOne(op => op.DeviceType).IsRequired(true).HasForeignKey(@"DeviceTypeId");

            #endregion

            #region Report Navigation properties

            modelBuilder.Entity<Report>().HasOne(x => x.Reporter).WithMany(op => op.MalfunctionReports).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");
            modelBuilder.Entity<Report>().HasOne(x => x.DeviceStatus).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceStatusId");
            modelBuilder.Entity<Report>().HasMany(x => x.AppendixReports).WithOne(op => op.ParentReport).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"AppendixReportId");
            modelBuilder.Entity<Report>().HasOne(x => x.ParentReport).WithMany(op => op.AppendixReports).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"AppendixReportId");
            modelBuilder.Entity<Report>().HasOne(x => x.DevicePlace).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DevicePlaceId");
            modelBuilder.Entity<Report>().HasOne(x => x.Wagon).WithMany(op => op.Reports).IsRequired(true).HasForeignKey(@"WagonId");
            modelBuilder.Entity<Report>().HasOne(x => x.WagonTrip).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"WagonTripId");

            #endregion

            #region Reporter Navigation properties

            modelBuilder.Entity<Reporter>().HasMany(x => x.MalfunctionReports).WithOne(op => op.Reporter).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");
            modelBuilder.Entity<Reporter>().HasOne(x => x.Department).WithMany(op => op.Reporters).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");
            modelBuilder.Entity<Reporter>().HasMany(x => x.TrainTrips).WithOne(op => op.Reporter).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");

            #endregion

            #region DeviceStatus Navigation properties

            modelBuilder.Entity<DeviceStatus>().HasMany(x => x.Reports).WithOne(op => op.DeviceStatus).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceStatusId");
            modelBuilder.Entity<DeviceStatus>().HasOne(x => x.DeviceType).WithMany(op => op.DeviceStatus).IsRequired(true).HasForeignKey(@"DeviceTypeId");

            #endregion

            #region WagonTrip Navigation properties

            modelBuilder.Entity<WagonTrip>().HasOne(x => x.Wagon).WithMany(op => op.Trips).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"WagonId");
            modelBuilder.Entity<WagonTrip>().HasOne(x => x.TrainTrip).WithMany(op => op.WagonsOfTrip).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainTripId");
            modelBuilder.Entity<WagonTrip>().HasMany(x => x.Reports).WithOne(op => op.WagonTrip).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"WagonTripId");

            #endregion

            #region Department Navigation properties

            modelBuilder.Entity<Department>().HasMany(x => x.DeviceTypes).WithOne(op => op.Department).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");
            modelBuilder.Entity<Department>().HasMany(x => x.Reporters).WithOne(op => op.Department).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");

            #endregion

            #region TrainTrip Navigation properties

            modelBuilder.Entity<TrainTrip>().HasOne(x => x.Train).WithMany(op => op.TrainTrips).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainId");
            modelBuilder.Entity<TrainTrip>().HasMany(x => x.WagonsOfTrip).WithOne(op => op.TrainTrip).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainTripId");
            modelBuilder.Entity<TrainTrip>().HasOne(x => x.Reporter).WithMany(op => op.TrainTrips).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");

            #endregion

            #region DevicePlace Navigation properties

            modelBuilder.Entity<DevicePlace>().HasOne(x => x.DeviceType).WithMany(op => op.DevicePlaces).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceTypeId");
            modelBuilder.Entity<DevicePlace>().HasMany(x => x.Reports).WithOne(op => op.DevicePlace).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DevicePlaceId");

            #endregion
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
