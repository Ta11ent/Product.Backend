﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductCatalog.Data;

#nullable disable

namespace ProductCatalog.Persistence.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20240725210755_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductCatalog.Domain.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Cost", b =>
                {
                    b.Property<Guid>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatePrice")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PriceId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PriceId")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Currency", b =>
                {
                    b.Property<Guid>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CurrencyId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("CurrencyId")
                        .IsUnique();

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Manufacturer", b =>
                {
                    b.Property<Guid>("ManufacturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ManufacturerId");

                    b.HasIndex("ManufacturerId")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Manufacturer");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid>("ManufacturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("SubCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductCatalog.Domain.SubCategory", b =>
                {
                    b.Property<Guid>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SubCategoryId")
                        .IsUnique();

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Cost", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Currency", "Currency")
                        .WithMany("Costs")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductCatalog.Domain.Product", "Product")
                        .WithMany("Costs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductCatalog.Domain.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("ProductCatalog.Domain.SubCategory", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Currency", b =>
                {
                    b.Navigation("Costs");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product", b =>
                {
                    b.Navigation("Costs");
                });

            modelBuilder.Entity("ProductCatalog.Domain.SubCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
