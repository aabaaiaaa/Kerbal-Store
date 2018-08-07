﻿// <auto-generated />
using KerbalStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace KerbalStore.Migrations
{
    [DbContext(typeof(KerbalStoreContext))]
    partial class KerbalStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KerbalStore.Data.Entities.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.Property<DateTime>("TokenExpiry");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("KerbalStore.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("OrderCreated");

                    b.Property<string>("OrderReference")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("KerbalStore.Data.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OrderId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("RocketPartId");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("RocketPartId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("KerbalStore.Data.Entities.RocketPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PartName");

                    b.Property<decimal>("Price");

                    b.Property<string>("Summary");

                    b.HasKey("Id");

                    b.ToTable("RocketParts");
                });

            modelBuilder.Entity("KerbalStore.Data.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FromWho");

                    b.Property<string>("PartRequested");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("KerbalStore.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("KerbalStore.Data.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("KerbalStore.Data.Entities.RocketPart", "RocketPart")
                        .WithMany()
                        .HasForeignKey("RocketPartId");
                });
#pragma warning restore 612, 618
        }
    }
}
