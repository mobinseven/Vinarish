﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VinarishMvc.Data;

namespace VinarishMvc.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190628135120_ParentReportId")]
    partial class ParentReportId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VinarishMvc.Areas.Authentication.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConfirmPassword");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("OldPassword");

                    b.Property<string>("Password");

                    b.Property<string>("ProfilePicture");

                    b.Property<string>("VinarishUserId");

                    b.HasKey("UserProfileId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("VinarishMvc.Areas.Identity.Models.VinarishUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("VinarishMvc.Models.Department", b =>
                {
                    b.Property<Guid>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DepartmentId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name");

                    b.HasKey("DepartmentId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("VinarishMvc.Models.DevicePlace", b =>
                {
                    b.Property<Guid>("DevicePlaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DevicePlaceId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description");

                    b.Property<Guid>("DeviceTypeId")
                        .IsConcurrencyToken()
                        .HasColumnName("DeviceTypeId");

                    b.HasKey("DevicePlaceId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("DevicePlaces");
                });

            modelBuilder.Entity("VinarishMvc.Models.DeviceStatus", b =>
                {
                    b.Property<Guid>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StatusId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Code");

                    b.Property<int?>("DeviceStatusType")
                        .HasColumnName("DeviceStatusType");

                    b.Property<Guid>("DeviceTypeId")
                        .HasColumnName("DeviceTypeId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("Text");

                    b.HasKey("StatusId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("DeviceStatus");
                });

            modelBuilder.Entity("VinarishMvc.Models.DeviceType", b =>
                {
                    b.Property<Guid>("DeviceTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DeviceTypeId");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnName("DepartmentId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name");

                    b.HasKey("DeviceTypeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("VinarishMvc.Models.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReportId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTimeCreated")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateTimeModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("DateTimeModified")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid>("DevicePlaceId")
                        .HasColumnName("DevicePlaceId");

                    b.Property<Guid>("DeviceStatusId")
                        .HasColumnName("DeviceStatusId");

                    b.Property<int?>("ParentReportId")
                        .HasColumnName("ParentReportId");

                    b.Property<int>("ReporterId")
                        .HasColumnName("ReporterId");

                    b.Property<int>("Status")
                        .HasColumnName("Status");

                    b.Property<Guid>("WagonId")
                        .HasColumnName("WagonId");

                    b.Property<Guid?>("WagonTripId")
                        .HasColumnName("WagonTripId");

                    b.HasKey("ReportId");

                    b.HasIndex("DevicePlaceId");

                    b.HasIndex("DeviceStatusId");

                    b.HasIndex("ParentReportId");

                    b.HasIndex("ReporterId");

                    b.HasIndex("WagonId");

                    b.HasIndex("WagonTripId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("VinarishMvc.Models.Reporter", b =>
                {
                    b.Property<int>("ReporterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReporterId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("DepartmentId")
                        .HasColumnName("DepartmentId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("UserName");

                    b.Property<string>("VinarishUserId")
                        .IsRequired()
                        .HasColumnName("VinarishUserId");

                    b.HasKey("ReporterId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("VinarishUserId")
                        .IsUnique();

                    b.ToTable("Reporters");
                });

            modelBuilder.Entity("VinarishMvc.Models.Train", b =>
                {
                    b.Property<int>("TrainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TrainId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name");

                    b.HasKey("TrainId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("VinarishMvc.Models.TrainTrip", b =>
                {
                    b.Property<Guid>("TrainTripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TrainTripId");

                    b.Property<DateTime>("DateTime")
                        .HasColumnName("DateTime");

                    b.Property<int>("ReporterId")
                        .HasColumnName("ReporterId");

                    b.Property<int>("TrainId")
                        .HasColumnName("TrainId");

                    b.HasKey("TrainTripId");

                    b.HasIndex("ReporterId");

                    b.HasIndex("TrainId");

                    b.ToTable("TrainTrips");
                });

            modelBuilder.Entity("VinarishMvc.Models.Wagon", b =>
                {
                    b.Property<Guid>("WagonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WagonId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name");

                    b.Property<int>("Number")
                        .HasColumnName("Number");

                    b.HasKey("WagonId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Wagons");
                });

            modelBuilder.Entity("VinarishMvc.Models.WagonTrip", b =>
                {
                    b.Property<Guid>("WagonTripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WagonTripId");

                    b.Property<Guid>("TrainTripId")
                        .HasColumnName("TrainTripId");

                    b.Property<Guid>("WagonId")
                        .HasColumnName("WagonId");

                    b.HasKey("WagonTripId");

                    b.HasIndex("TrainTripId");

                    b.HasIndex("WagonId");

                    b.ToTable("WagonTrips");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VinarishMvc.Areas.Identity.Models.VinarishUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VinarishMvc.Areas.Identity.Models.VinarishUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VinarishMvc.Areas.Identity.Models.VinarishUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("VinarishMvc.Areas.Identity.Models.VinarishUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VinarishMvc.Models.DevicePlace", b =>
                {
                    b.HasOne("VinarishMvc.Models.DeviceType", "DeviceType")
                        .WithMany("DevicePlaces")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("VinarishMvc.Models.DeviceStatus", b =>
                {
                    b.HasOne("VinarishMvc.Models.DeviceType", "DeviceType")
                        .WithMany("DeviceStatus")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VinarishMvc.Models.DeviceType", b =>
                {
                    b.HasOne("VinarishMvc.Models.Department", "Department")
                        .WithMany("DeviceTypes")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("VinarishMvc.Models.Report", b =>
                {
                    b.HasOne("VinarishMvc.Models.DevicePlace", "DevicePlace")
                        .WithMany("Reports")
                        .HasForeignKey("DevicePlaceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("VinarishMvc.Models.DeviceStatus", "DeviceStatus")
                        .WithMany("Reports")
                        .HasForeignKey("DeviceStatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("VinarishMvc.Models.Report", "ParentReport")
                        .WithMany("AppendixReports")
                        .HasForeignKey("ParentReportId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("VinarishMvc.Models.Reporter", "Reporter")
                        .WithMany("MalfunctionReports")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("VinarishMvc.Models.Wagon", "Wagon")
                        .WithMany("Reports")
                        .HasForeignKey("WagonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VinarishMvc.Models.WagonTrip", "WagonTrip")
                        .WithMany("Reports")
                        .HasForeignKey("WagonTripId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("VinarishMvc.Models.Reporter", b =>
                {
                    b.HasOne("VinarishMvc.Models.Department", "Department")
                        .WithMany("Reporters")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("VinarishMvc.Models.TrainTrip", b =>
                {
                    b.HasOne("VinarishMvc.Models.Reporter", "Reporter")
                        .WithMany("TrainTrips")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("VinarishMvc.Models.Train", "Train")
                        .WithMany("TrainTrips")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("VinarishMvc.Models.WagonTrip", b =>
                {
                    b.HasOne("VinarishMvc.Models.TrainTrip", "TrainTrip")
                        .WithMany("WagonsOfTrip")
                        .HasForeignKey("TrainTripId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("VinarishMvc.Models.Wagon", "Wagon")
                        .WithMany("Trips")
                        .HasForeignKey("WagonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
