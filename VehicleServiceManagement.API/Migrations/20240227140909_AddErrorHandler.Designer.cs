﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleServiceManagement.API.Data;

#nullable disable

namespace VehicleServiceManagement.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240227140909_AddErrorHandler")]
    partial class AddErrorHandler
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Error", b =>
                {
                    b.Property<int>("ErrorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ErrorId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaclTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ErrorId");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"), 1L, 1);

                    b.Property<DateTime>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("FeedbackId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Material", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"), 1L, 1);

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ItemID");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CardNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ScheduledService", b =>
                {
                    b.Property<int>("ScheduledServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduledServiceId"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ServiceAdvisorID")
                        .HasColumnType("int");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.HasKey("ScheduledServiceId");

                    b.HasIndex("ServiceAdvisorID");

                    b.HasIndex("VehicleID");

                    b.ToTable("ScheduledServices");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ServiceRecord", b =>
                {
                    b.Property<int>("ServiceRecordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceRecordID"), 1L, 1);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RepresentativeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ServiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.HasKey("ServiceRecordID");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RepresentativeID");

                    b.HasIndex("VehicleID");

                    b.ToTable("ServiceRecords");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ServiceRecordItem", b =>
                {
                    b.Property<int>("ServiceRecordItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceRecordItemId"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ServiceRecordID")
                        .HasColumnType("int");

                    b.HasKey("ServiceRecordItemId");

                    b.HasIndex("ItemID");

                    b.HasIndex("ServiceRecordID");

                    b.ToTable("ServiceRecordItems");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ServiceRepresentative", b =>
                {
                    b.Property<int>("RepresentativeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RepresentativeID"), 1L, 1);

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RepresentativeID");

                    b.ToTable("ServiceRepresentatives");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VehicleBrand")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("VehicleCategory")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("VehicleRegNo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Feedback", b =>
                {
                    b.HasOne("VehicleServiceManagement.API.Models.Domain.Customer", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Payment", b =>
                {
                    b.HasOne("VehicleServiceManagement.API.Models.Domain.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ScheduledService", b =>
                {
                    b.HasOne("VehicleServiceManagement.API.Models.Domain.ServiceRepresentative", "ServiceRepresentative")
                        .WithMany("ScheduledServices")
                        .HasForeignKey("ServiceAdvisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleServiceManagement.API.Models.Domain.Vehicle", "Vehicle")
                        .WithMany("ScheduledServices")
                        .HasForeignKey("VehicleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceRepresentative");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ServiceRecord", b =>
                {
                    b.HasOne("VehicleServiceManagement.API.Models.Domain.Customer", "Customer")
                        .WithMany("ServiceRecords")
                        .HasForeignKey("CustomerId");

                    b.HasOne("VehicleServiceManagement.API.Models.Domain.ServiceRepresentative", "Representative")
                        .WithMany("ServiceRecords")
                        .HasForeignKey("RepresentativeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleServiceManagement.API.Models.Domain.Vehicle", "Vehicle")
                        .WithMany("ServiceRecords")
                        .HasForeignKey("VehicleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Representative");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ServiceRecordItem", b =>
                {
                    b.HasOne("VehicleServiceManagement.API.Models.Domain.Material", "Material")
                        .WithMany("ServiceRecordItems")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleServiceManagement.API.Models.Domain.ServiceRecord", "ServiceRecord")
                        .WithMany("ServiceRecordItems")
                        .HasForeignKey("ServiceRecordID");

                    b.Navigation("Material");

                    b.Navigation("ServiceRecord");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Customer", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Payments");

                    b.Navigation("ServiceRecords");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Material", b =>
                {
                    b.Navigation("ServiceRecordItems");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ServiceRecord", b =>
                {
                    b.Navigation("ServiceRecordItems");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.ServiceRepresentative", b =>
                {
                    b.Navigation("ScheduledServices");

                    b.Navigation("ServiceRecords");
                });

            modelBuilder.Entity("VehicleServiceManagement.API.Models.Domain.Vehicle", b =>
                {
                    b.Navigation("ScheduledServices");

                    b.Navigation("ServiceRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
