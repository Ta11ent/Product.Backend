﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductCatalog.Data;

#nullable disable

namespace ProductCatalog.Persistence.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                    b.Property<Guid>("CostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatePrice")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductSaleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CostId");

                    b.HasIndex("CostId")
                        .IsUnique();

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ProductSaleId");

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

                    b.HasKey("ProductId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductCatalog.Domain.ProductSale", b =>
                {
                    b.Property<Guid>("ProductSaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductSaleId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("ProductSaleId")
                        .IsUnique();

                    b.HasIndex("SubCategoryId");

                    b.ToTable("ProductSale");
                });

            modelBuilder.Entity("ProductCatalog.Domain.ROE", b =>
                {
                    b.Property<Guid>("ROEId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrecnyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Rate")
                        .HasPrecision(18, 8)
                        .HasColumnType("decimal(18,8)");

                    b.HasKey("ROEId");

                    b.HasIndex("CurrecnyId");

                    b.HasIndex("ROEId")
                        .IsUnique();

                    b.ToTable("ROE");
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
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductCatalog.Domain.ProductSale", "ProductSale")
                        .WithMany("Costs")
                        .HasForeignKey("ProductSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("ProductSale");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("ProductCatalog.Domain.ProductSale", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Product", "Product")
                        .WithOne("ProductSale")
                        .HasForeignKey("ProductCatalog.Domain.ProductSale", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductCatalog.Domain.SubCategory", "SubCategory")
                        .WithMany("ProductsForSale")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("ProductCatalog.Domain.ROE", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Currency", "Currency")
                        .WithMany("ROEs")
                        .HasForeignKey("CurrecnyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
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
                    b.Navigation("ROEs");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product", b =>
                {
                    b.Navigation("ProductSale")
                        .IsRequired();
                });

            modelBuilder.Entity("ProductCatalog.Domain.ProductSale", b =>
                {
                    b.Navigation("Costs");
                });

            modelBuilder.Entity("ProductCatalog.Domain.SubCategory", b =>
                {
                    b.Navigation("ProductsForSale");
                });
#pragma warning restore 612, 618
        }
    }
}