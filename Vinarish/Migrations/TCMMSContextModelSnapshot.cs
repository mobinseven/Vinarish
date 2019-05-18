﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vinarish.Data;

namespace Vinarish.Migrations
{
    [DbContext(typeof(TCMMSContext))]
    partial class TCMMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vinarish.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnName("CategoryName");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("DepartmentId");

                    b.Property<long?>("OrderNumber")
                        .HasColumnName("OrderNumber");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("OrderNumber")
                        .IsUnique()
                        .HasFilter("[OrderNumber] IS NOT NULL");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Vinarish.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Vinarish.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .IsUnicode(true);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Vinarish.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Code");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("Text");

                    b.HasKey("Id");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("Vinarish.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CatId");

                    b.Property<int>("CodeId");

                    b.Property<DateTime>("DateTime")
                        .HasColumnName("DateTime");

                    b.Property<int>("PlaceId");

                    b.Property<int>("ReporterId");

                    b.Property<int>("WagonId");

                    b.HasKey("Id");

                    b.HasIndex("CatId");

                    b.HasIndex("CodeId");

                    b.HasIndex("PlaceId");

                    b.HasIndex("ReporterId");

                    b.HasIndex("WagonId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("Vinarish.Models.StatCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CatId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Code");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("Text");

                    b.HasKey("Id");

                    b.HasIndex("CatId");

                    b.ToTable("StatCode");
                });

            modelBuilder.Entity("Vinarish.Models.Train", b =>
                {
                    b.Property<int>("TrainId");

                    b.Property<int?>("HeadId");

                    b.Property<int?>("OfficerId");

                    b.HasKey("TrainId");

                    b.HasIndex("HeadId");

                    b.HasIndex("OfficerId");

                    b.HasIndex("TrainId")
                        .IsUnique();

                    b.ToTable("Train");
                });

            modelBuilder.Entity("Vinarish.Models.Wagon", b =>
                {
                    b.Property<int>("WagonId");

                    b.Property<int>("TrainId");

                    b.HasKey("WagonId");

                    b.HasIndex("TrainId");

                    b.HasIndex("WagonId")
                        .IsUnique();

                    b.ToTable("Wagon");
                });

            modelBuilder.Entity("Vinarish.Models.Category", b =>
                {
                    b.HasOne("Vinarish.Models.Department", "Department")
                        .WithMany("Category")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Category_Department");
                });

            modelBuilder.Entity("Vinarish.Models.Person", b =>
                {
                    b.HasOne("Vinarish.Models.Department", "Department")
                        .WithMany("Person")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Persons_Department");
                });

            modelBuilder.Entity("Vinarish.Models.Report", b =>
                {
                    b.HasOne("Vinarish.Models.Category", "Cat")
                        .WithMany("Report")
                        .HasForeignKey("CatId")
                        .HasConstraintName("FK_Report_Cat");

                    b.HasOne("Vinarish.Models.StatCode", "Code")
                        .WithMany("Report")
                        .HasForeignKey("CodeId")
                        .HasConstraintName("FK_Report_CodeId");

                    b.HasOne("Vinarish.Models.Place", "Place")
                        .WithMany("Report")
                        .HasForeignKey("PlaceId")
                        .HasConstraintName("FK_Report_PlaceId");

                    b.HasOne("Vinarish.Models.Person", "Reporter")
                        .WithMany("Report")
                        .HasForeignKey("ReporterId")
                        .HasConstraintName("FK_Report_Reporter");

                    b.HasOne("Vinarish.Models.Wagon", "Wagon")
                        .WithMany("Report")
                        .HasForeignKey("WagonId")
                        .HasConstraintName("FK_Report_Wagon");
                });

            modelBuilder.Entity("Vinarish.Models.StatCode", b =>
                {
                    b.HasOne("Vinarish.Models.Category", "Cat")
                        .WithMany("StatCode")
                        .HasForeignKey("CatId")
                        .HasConstraintName("FK_Code_Cat");
                });

            modelBuilder.Entity("Vinarish.Models.Train", b =>
                {
                    b.HasOne("Vinarish.Models.Person", "Head")
                        .WithMany("TrainHead")
                        .HasForeignKey("HeadId")
                        .HasConstraintName("FK_Trains_Head");

                    b.HasOne("Vinarish.Models.Person", "Officer")
                        .WithMany("TrainOfficer")
                        .HasForeignKey("OfficerId")
                        .HasConstraintName("FK_Trains_ElecOfficer");
                });

            modelBuilder.Entity("Vinarish.Models.Wagon", b =>
                {
                    b.HasOne("Vinarish.Models.Train", "Train")
                        .WithMany("Wagon")
                        .HasForeignKey("TrainId")
                        .HasConstraintName("FK_Wagon_Train");
                });
#pragma warning restore 612, 618
        }
    }
}
