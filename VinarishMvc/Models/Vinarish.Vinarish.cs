﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/29/2019 9:55:51 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using VinarishMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VinarishMvc.Areas.Authentication.Models;
using VinarishMvc.Areas.Identity.Models;
using System.ComponentModel.DataAnnotations;

namespace VinarishMvc.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<VinarishUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CustomizeConfiguration(ref optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
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

        public virtual DbSet<Site> Sites { get; set; }

        public virtual DbSet<Assistant> Assistants { get; set; }

        protected void VinarishOnModelCreating(ModelBuilder modelBuilder)
        {
            this.TrainMapping(modelBuilder);
            this.CustomizeTrainMapping(modelBuilder);

            this.WagonMapping(modelBuilder);
            this.CustomizeWagonMapping(modelBuilder);

            this.DeviceTypeMapping(modelBuilder);
            this.CustomizeDeviceTypeMapping(modelBuilder);

            this.ReportMapping(modelBuilder);
            this.CustomizeReportMapping(modelBuilder);

            this.ReporterMapping(modelBuilder);
            this.CustomizeReporterMapping(modelBuilder);

            this.SiteMapping(modelBuilder);
            this.CustomizeSiteMapping(modelBuilder);

            this.DeviceStatusMapping(modelBuilder);
            this.CustomizeDeviceStatusMapping(modelBuilder);

            this.WagonTripMapping(modelBuilder);
            this.CustomizeWagonTripMapping(modelBuilder);

            this.DepartmentMapping(modelBuilder);
            this.CustomizeDepartmentMapping(modelBuilder);

            this.TrainTripMapping(modelBuilder);
            this.CustomizeTrainTripMapping(modelBuilder);

            this.DevicePlaceMapping(modelBuilder);
            this.CustomizeDevicePlaceMapping(modelBuilder);

            this.AssistantMapping(modelBuilder);

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

        #endregion Train Mapping

        #region Wagon Mapping

        private void WagonMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wagon>().ToTable(@"Wagons");
            modelBuilder.Entity<Wagon>().Property<System.Guid>(x => x.WagonId).HasColumnName(@"WagonId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Wagon>().Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Wagon>().HasKey(@"WagonId");
            modelBuilder.Entity<Wagon>().HasIndex(@"Name").IsUnique(true);
        }

        partial void CustomizeWagonMapping(ModelBuilder modelBuilder);

        #endregion Wagon Mapping

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

        #endregion DeviceType Mapping

        #region Report Mapping

        private void ReportMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable(@"Reports");
            modelBuilder.Entity<Report>().Property<int>(x => x.ReportId).HasColumnName(@"ReportId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Report>().Property<System.DateTime>(x => x.DateTimeCreated).HasColumnName(@"DateTimeCreated").IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql(@"CURRENT_TIMESTAMP");
            modelBuilder.Entity<Report>().Property<System.DateTime>(x => x.DateTimeModified).HasColumnName(@"DateTimeModified").IsRequired().ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(@"CURRENT_TIMESTAMP");
            modelBuilder.Entity<Report>().Property<int>(x => x.ReporterId).HasColumnName(@"ReporterId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<int?>(x => x.SiteId).HasColumnName(@"SiteId").ValueGeneratedNever();
            //modelBuilder.Entity<Report>().Property<VinarishMvc.Models.ReportStatus>(x => x.Status).HasColumnName(@"Status").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid>(x => x.DeviceStatusId).HasColumnName(@"DeviceStatusId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property(x => x.ParentReportId).HasColumnName(@"ParentReportId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid>(x => x.DevicePlaceId).HasColumnName(@"DevicePlaceId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid>(x => x.WagonId).HasColumnName(@"WagonId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<System.Guid?>(x => x.WagonTripId).HasColumnName(@"WagonTripId").ValueGeneratedNever();
            modelBuilder.Entity<Report>().Property<string>(x => x.Code).HasColumnName(@"Code").ValueGeneratedNever();
            modelBuilder.Entity<Report>().HasKey(@"ReportId");
            modelBuilder.Entity<Report>().HasIndex(x => x.Code).IsUnique(true);
        }

        partial void CustomizeReportMapping(ModelBuilder modelBuilder);

        #endregion Report Mapping

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

        #endregion Reporter Mapping

        #region Site Mapping

        private void SiteMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>().ToTable(@"Sites");
            modelBuilder.Entity<Site>().Property<int>(x => x.SiteId).HasColumnName(@"SiteId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Site>().Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Site>().HasKey(@"SiteId");
        }

        partial void CustomizeSiteMapping(ModelBuilder modelBuilder);

        #endregion Site Mapping

        #region DeviceStatus Mapping

        private void DeviceStatusMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceStatus>().ToTable(@"DeviceStatus");
            modelBuilder.Entity<DeviceStatus>().Property<System.Guid>(x => x.StatusId).HasColumnName(@"StatusId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<DeviceStatus>().Property<string>(x => x.Code).HasColumnName(@"Code").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().Property<string>(x => x.Text).HasColumnName(@"Text").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().Property<VinarishMvc.Models.DeviceStatusType?>(x => x.DeviceStatusType).HasColumnName(@"DeviceStatusType").ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().Property<System.Guid?>(x => x.DeviceTypeId).HasColumnName(@"DeviceTypeId").ValueGeneratedNever();
            modelBuilder.Entity<DeviceStatus>().HasKey(@"StatusId");
            modelBuilder.Entity<DeviceStatus>().HasIndex(@"Code").IsUnique(true);
        }

        partial void CustomizeDeviceStatusMapping(ModelBuilder modelBuilder);

        #endregion DeviceStatus Mapping

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

        #endregion WagonTrip Mapping

        #region Assistant Mapping

        private void AssistantMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assistant>().ToTable(@"Assistants");
            modelBuilder.Entity<Assistant>().Property<int>(x => x.AssistantId).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Assistant>().Property<int>(x => x.PersonId).ValueGeneratedNever();
            modelBuilder.Entity<Assistant>().Property<int>(x => x.ReportId).ValueGeneratedNever();
            modelBuilder.Entity<Assistant>().HasKey(x => x.AssistantId);
        }

        #endregion Assistant Mapping

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

        #endregion Department Mapping

        #region TrainTrip Mapping

        private void TrainTripMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainTrip>().ToTable(@"TrainTrips");
            modelBuilder.Entity<TrainTrip>().Property<System.Guid>(x => x.TrainTripId).HasColumnName(@"TrainTripId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<TrainTrip>().Property<System.DateTime>(x => x.DateTime).HasColumnName(@"DateTime").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<TrainTrip>().Property<int>(x => x.TrainId).HasColumnName(@"TrainId").ValueGeneratedNever();
            //modelBuilder.Entity<TrainTrip>().Property<int>(x => x.ReporterId).HasColumnName(@"ReporterId").ValueGeneratedNever();
            modelBuilder.Entity<TrainTrip>().HasKey(@"TrainTripId");
        }

        partial void CustomizeTrainTripMapping(ModelBuilder modelBuilder);

        #endregion TrainTrip Mapping

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

        #endregion DevicePlace Mapping

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            #region Train Navigation properties

            modelBuilder.Entity<Train>().HasMany(x => x.TrainTrips).WithOne(op => op.Train).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainId");

            #endregion Train Navigation properties

            #region Wagon Navigation properties

            modelBuilder.Entity<Wagon>().HasMany(x => x.Trips).WithOne(op => op.Wagon).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"WagonId");
            modelBuilder.Entity<Wagon>().HasMany(x => x.Reports).WithOne(op => op.Wagon).IsRequired(true).HasForeignKey(@"WagonId");

            #endregion Wagon Navigation properties

            #region DeviceType Navigation properties

            modelBuilder.Entity<DeviceType>().HasOne(x => x.Department).WithMany(op => op.DeviceTypes).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");
            modelBuilder.Entity<DeviceType>().HasMany(x => x.DevicePlaces).WithOne(op => op.DeviceType).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceTypeId");
            modelBuilder.Entity<DeviceType>().HasMany(x => x.DeviceStatus).WithOne(op => op.DeviceType).IsRequired(false).HasForeignKey(@"DeviceTypeId");

            #endregion DeviceType Navigation properties

            #region Report Navigation properties

            modelBuilder.Entity<Report>().HasOne(x => x.Reporter).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");
            modelBuilder.Entity<Report>().HasOne(x => x.DeviceStatus).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceStatusId");
            modelBuilder.Entity<Report>().HasMany(x => x.AppendixReports).WithOne(op => op.ParentReport).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"ParentReportId");
            modelBuilder.Entity<Report>().HasOne(x => x.ParentReport).WithMany(op => op.AppendixReports).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"ParentReportId");
            modelBuilder.Entity<Report>().HasOne(x => x.DevicePlace).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DevicePlaceId");
            modelBuilder.Entity<Report>().HasOne(x => x.Wagon).WithMany(op => op.Reports).IsRequired(true).HasForeignKey(@"WagonId");
            modelBuilder.Entity<Report>().HasOne(x => x.WagonTrip).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"WagonTripId");
            modelBuilder.Entity<Report>().HasOne(x => x.Site).WithMany(op => op.Reports).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"SiteId");
            modelBuilder.Entity<Report>().HasMany(x => x.Assistants).WithOne(op => op.Report).OnDelete(DeleteBehavior.Restrict).HasForeignKey(op => op.AssistantId);

            #endregion Report Navigation properties

            #region Reporter Navigation properties

            modelBuilder.Entity<Reporter>().HasMany(x => x.Reports).WithOne(op => op.Reporter).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");
            modelBuilder.Entity<Reporter>().HasOne(x => x.Department).WithMany(op => op.Reporters).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");
            //modelBuilder.Entity<Reporter>().HasMany(x => x.TrainTrips).WithOne(op => op.Reporter).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");
            modelBuilder.Entity<Reporter>().HasMany(x => x.AsAssistant).WithOne(op => op.Person).OnDelete(DeleteBehavior.Restrict).HasForeignKey(op => op.AssistantId);

            #endregion Reporter Navigation properties

            #region Site Navigation properties

            modelBuilder.Entity<Site>().HasMany(x => x.Reports).WithOne(op => op.Site).OnDelete(DeleteBehavior.Restrict).HasForeignKey(op => op.SiteId);

            #endregion Site Navigation properties

            #region DeviceStatus Navigation properties

            modelBuilder.Entity<DeviceStatus>().HasMany(x => x.Reports).WithOne(op => op.DeviceStatus).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceStatusId");
            modelBuilder.Entity<DeviceStatus>().HasOne(x => x.DeviceType).WithMany(op => op.DeviceStatus).IsRequired(false).HasForeignKey(@"DeviceTypeId");

            #endregion DeviceStatus Navigation properties

            #region WagonTrip Navigation properties

            modelBuilder.Entity<WagonTrip>().HasOne(x => x.Wagon).WithMany(op => op.Trips).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"WagonId");
            modelBuilder.Entity<WagonTrip>().HasOne(x => x.TrainTrip).WithMany(op => op.WagonsOfTrip).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainTripId");
            modelBuilder.Entity<WagonTrip>().HasMany(x => x.Reports).WithOne(op => op.WagonTrip).OnDelete(DeleteBehavior.Restrict).IsRequired(false).HasForeignKey(@"WagonTripId");

            #endregion WagonTrip Navigation properties

            #region Assistant Navigation properties

            modelBuilder.Entity<Assistant>().HasOne(x => x.Report).WithMany(op => op.Assistants).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(x => x.ReportId);
            modelBuilder.Entity<Assistant>().HasOne(x => x.Person).WithMany(op => op.AsAssistant).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(x => x.PersonId);

            #endregion Assistant Navigation properties

            #region Department Navigation properties

            modelBuilder.Entity<Department>().HasMany(x => x.DeviceTypes).WithOne(op => op.Department).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");
            modelBuilder.Entity<Department>().HasMany(x => x.Reporters).WithOne(op => op.Department).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DepartmentId");

            #endregion Department Navigation properties

            #region TrainTrip Navigation properties

            modelBuilder.Entity<TrainTrip>().HasOne(x => x.Train).WithMany(op => op.TrainTrips).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainId");
            modelBuilder.Entity<TrainTrip>().HasMany(x => x.WagonsOfTrip).WithOne(op => op.TrainTrip).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"TrainTripId");
            //modelBuilder.Entity<TrainTrip>().HasOne(x => x.Reporter).WithMany(op => op.TrainTrips).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"ReporterId");

            #endregion TrainTrip Navigation properties

            #region DevicePlace Navigation properties

            modelBuilder.Entity<DevicePlace>().HasOne(x => x.DeviceType).WithMany(op => op.DevicePlaces).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DeviceTypeId");
            modelBuilder.Entity<DevicePlace>().HasMany(x => x.Reports).WithOne(op => op.DevicePlace).OnDelete(DeleteBehavior.Restrict).IsRequired(true).HasForeignKey(@"DevicePlaceId");

            #endregion DevicePlace Navigation properties
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}