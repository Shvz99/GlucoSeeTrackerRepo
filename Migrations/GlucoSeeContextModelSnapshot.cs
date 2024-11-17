﻿// <auto-generated />
using System;
using GlucoSeeTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GlucoSeeTracker.Migrations
{
    [DbContext(typeof(GlucoSeeContext))]
    partial class GlucoSeeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GlucoSeeTracker.Models.Dashboard", b =>
                {
                    b.Property<int>("DashID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DashID"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LastReading")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("DashID");

                    b.HasIndex("UserID");

                    b.ToTable("Dashboards");

                    b.HasData(
                        new
                        {
                            DashID = 1,
                            Age = 40,
                            FirstName = "Will",
                            LastName = "Smith",
                            LastReading = 7m,
                            UserID = 1
                        },
                        new
                        {
                            DashID = 2,
                            Age = 30,
                            FirstName = "Sam",
                            LastName = "Smith",
                            LastReading = 8.5m,
                            UserID = 2
                        },
                        new
                        {
                            DashID = 3,
                            Age = 30,
                            FirstName = "Taylor",
                            LastName = "Swift",
                            LastReading = 5.6m,
                            UserID = 3
                        });
                });

            modelBuilder.Entity("GlucoSeeTracker.Models.GlucoRecord", b =>
                {
                    b.Property<int>("ReadingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReadingID"), 1L, 1);

                    b.Property<int>("DashID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GlucoLevel")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PrePostMeal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReadingID");

                    b.HasIndex("DashID");

                    b.ToTable("GlucoRecords");

                    b.HasData(
                        new
                        {
                            ReadingID = 1,
                            DashID = 1,
                            DateTime = new DateTime(2022, 3, 12, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            GlucoLevel = 7.5m,
                            PrePostMeal = "Before"
                        },
                        new
                        {
                            ReadingID = 2,
                            DashID = 2,
                            DateTime = new DateTime(2023, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            GlucoLevel = 8.5m,
                            PrePostMeal = "After"
                        },
                        new
                        {
                            ReadingID = 3,
                            DashID = 3,
                            DateTime = new DateTime(2023, 9, 17, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            GlucoLevel = 7m,
                            PrePostMeal = "After"
                        });
                });

            modelBuilder.Entity("GlucoSeeTracker.Models.Landing", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Landings");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Password = "Password1",
                            Username = "First"
                        },
                        new
                        {
                            UserID = 2,
                            Password = "Password2",
                            Username = "Second"
                        },
                        new
                        {
                            UserID = 3,
                            Password = "Password3",
                            Username = "Third"
                        });
                });

            modelBuilder.Entity("GlucoSeeTracker.Models.Dashboard", b =>
                {
                    b.HasOne("GlucoSeeTracker.Models.Landing", "Landing")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Landing");
                });

            modelBuilder.Entity("GlucoSeeTracker.Models.GlucoRecord", b =>
                {
                    b.HasOne("GlucoSeeTracker.Models.Dashboard", "Dashboard")
                        .WithMany()
                        .HasForeignKey("DashID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dashboard");
                });
#pragma warning restore 612, 618
        }
    }
}