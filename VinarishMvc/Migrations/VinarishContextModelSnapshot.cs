﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VinarishMvc.Models;

namespace VinarishMvc.Migrations
{
    [DbContext(typeof(VinarishContext))]
    partial class VinarishContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VinarishMvc.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<int>("DepartmentId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("VinarishMvc.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("VinarishMvc.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("VinarishMvc.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("VinarishMvc.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppendixReportId");

                    b.Property<int>("CatId");

                    b.Property<int>("CodeId");

                    b.Property<DateTime>("DateTime");

                    b.Property<bool?>("IsValid");

                    b.Property<int>("PlaceId");

                    b.Property<int>("ReporterId");

                    b.Property<int>("WagonId");

                    b.HasKey("Id");

                    b.HasIndex("AppendixReportId");

                    b.HasIndex("CatId");

                    b.HasIndex("CodeId");

                    b.HasIndex("PlaceId");

                    b.HasIndex("ReporterId");

                    b.HasIndex("WagonId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("VinarishMvc.Models.StatCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CatId");

                    b.Property<string>("Code");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CatId");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("([Code] IS NOT NULL)");

                    b.ToTable("StatCode");
                });

            modelBuilder.Entity("VinarishMvc.Models.Train", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TrainId");

                    b.HasKey("Id");

                    b.HasIndex("TrainId")
                        .IsUnique();

                    b.ToTable("Train");
                });

            modelBuilder.Entity("VinarishMvc.Models.Wagon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TrainId");

                    b.Property<int>("WagonId");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.HasIndex("WagonId")
                        .IsUnique();

                    b.ToTable("Wagon");
                });

            modelBuilder.Entity("VinarishMvc.Models.Category", b =>
                {
                    b.HasOne("VinarishMvc.Models.Department", "Department")
                        .WithMany("Category")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Category_Department");
                });

            modelBuilder.Entity("VinarishMvc.Models.Person", b =>
                {
                    b.HasOne("VinarishMvc.Models.Department", "Department")
                        .WithMany("Person")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Persons_Department");
                });

            modelBuilder.Entity("VinarishMvc.Models.Report", b =>
                {
                    b.HasOne("VinarishMvc.Models.Report", "AppendixReport")
                        .WithMany("InverseAppendixReport")
                        .HasForeignKey("AppendixReportId")
                        .HasConstraintName("FK_Report_AppendixReport");

                    b.HasOne("VinarishMvc.Models.Category", "Cat")
                        .WithMany("Report")
                        .HasForeignKey("CatId")
                        .HasConstraintName("FK_Report_Cat");

                    b.HasOne("VinarishMvc.Models.StatCode", "Code")
                        .WithMany("Report")
                        .HasForeignKey("CodeId")
                        .HasConstraintName("FK_Report_CodeId");

                    b.HasOne("VinarishMvc.Models.Place", "Place")
                        .WithMany("Report")
                        .HasForeignKey("PlaceId")
                        .HasConstraintName("FK_Report_PlaceId");

                    b.HasOne("VinarishMvc.Models.Person", "Reporter")
                        .WithMany("Report")
                        .HasForeignKey("ReporterId")
                        .HasConstraintName("FK_Report_Reporter");

                    b.HasOne("VinarishMvc.Models.Wagon", "Wagon")
                        .WithMany("Report")
                        .HasForeignKey("WagonId")
                        .HasConstraintName("FK_Report_Wagon");
                });

            modelBuilder.Entity("VinarishMvc.Models.StatCode", b =>
                {
                    b.HasOne("VinarishMvc.Models.Category", "Cat")
                        .WithMany("StatCode")
                        .HasForeignKey("CatId")
                        .HasConstraintName("FK_Code_Cat");
                });

            modelBuilder.Entity("VinarishMvc.Models.Wagon", b =>
                {
                    b.HasOne("VinarishMvc.Models.Train", "Train")
                        .WithMany("Wagon")
                        .HasForeignKey("TrainId")
                        .HasConstraintName("FK_Wagon_Train");
                });
#pragma warning restore 612, 618
        }
    }
}