﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCart.Persistence;

#nullable disable

namespace ShoppingCart.Persistence.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20231203195244_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingCart.Domain.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShoppingCart.Domain.ProductRange", b =>
                {
                    b.Property<Guid>("ProductRangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductRangeId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductRangeId")
                        .IsUnique();

                    b.ToTable("ProductRanges");
                });

            modelBuilder.Entity("ShoppingCart.Domain.ProductRange", b =>
                {
                    b.HasOne("ShoppingCart.Domain.Order", "Order")
                        .WithMany("ProductRanges")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ShoppingCart.Domain.Order", b =>
                {
                    b.Navigation("ProductRanges");
                });
#pragma warning restore 612, 618
        }
    }
}