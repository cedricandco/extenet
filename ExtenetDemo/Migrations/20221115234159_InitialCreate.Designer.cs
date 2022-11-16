﻿// <auto-generated />
using System;
using Extenet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExtenetDemo.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20221115234159_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Extenet.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("Extenet.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"), 1L, 1);

                    b.Property<int?>("AdministratorID")
                        .HasColumnType("int");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int?>("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DepartmentID");

                    b.HasIndex("AdministratorID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Extenet.Models.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ItemID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Item", (string)null);
                });

            modelBuilder.Entity("Extenet.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("VendorID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("VendorID");

                    b.ToTable("OfficeAssignments");
                });

            modelBuilder.Entity("Extenet.Models.Sale", b =>
                {
                    b.Property<int>("SaleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleID"), 1L, 1);

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.HasKey("SaleID");

                    b.HasIndex("ClientID");

                    b.HasIndex("ItemID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Extenet.Models.Vendor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Vendor", (string)null);
                });

            modelBuilder.Entity("ItemVendor", b =>
                {
                    b.Property<int>("CoursesItemID")
                        .HasColumnType("int");

                    b.Property<int>("VendorsID")
                        .HasColumnType("int");

                    b.HasKey("CoursesItemID", "VendorsID");

                    b.HasIndex("VendorsID");

                    b.ToTable("ItemVendor");
                });

            modelBuilder.Entity("Extenet.Models.Department", b =>
                {
                    b.HasOne("Extenet.Models.Vendor", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID");

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("Extenet.Models.Item", b =>
                {
                    b.HasOne("Extenet.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Extenet.Models.OfficeAssignment", b =>
                {
                    b.HasOne("Extenet.Models.Vendor", "Vendor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("Extenet.Models.OfficeAssignment", "VendorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Extenet.Models.Sale", b =>
                {
                    b.HasOne("Extenet.Models.Client", "Client")
                        .WithMany("Sales")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Extenet.Models.Item", "Item")
                        .WithMany("Sales")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ItemVendor", b =>
                {
                    b.HasOne("Extenet.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("CoursesItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Extenet.Models.Vendor", null)
                        .WithMany()
                        .HasForeignKey("VendorsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Extenet.Models.Client", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Extenet.Models.Department", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Extenet.Models.Item", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Extenet.Models.Vendor", b =>
                {
                    b.Navigation("OfficeAssignment");
                });
#pragma warning restore 612, 618
        }
    }
}
