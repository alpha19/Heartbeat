﻿// <auto-generated />
using System;
using LabSystems.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LabSystems.Domain.Migrations
{
    [DbContext(typeof(LabSystemsContext))]
    [Migration("20210416014942_Init4")]
    partial class Init4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LabSystems.Domain.Models.DiskDrive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Firmware")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LabSystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModelNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LabSystemId");

                    b.ToTable("DiskDrives");
                });

            modelBuilder.Entity("LabSystems.Domain.Models.LabSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("HostName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ipaddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Osversion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LabSystems");
                });

            modelBuilder.Entity("LabSystems.Domain.Models.DiskDrive", b =>
                {
                    b.HasOne("LabSystems.Domain.Models.LabSystem", null)
                        .WithMany("DiskDrives")
                        .HasForeignKey("LabSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LabSystems.Domain.Models.LabSystem", b =>
                {
                    b.Navigation("DiskDrives");
                });
#pragma warning restore 612, 618
        }
    }
}
