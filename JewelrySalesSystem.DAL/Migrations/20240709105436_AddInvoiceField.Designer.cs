﻿// <auto-generated />
using System;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JewelrySalesSystem.DAL.Migrations
{
    [DbContext(typeof(JewelryDbContext))]
    [Migration("20240709105436_AddInvoiceField")]
    partial class AddInvoiceField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Carat", b =>
                {
                    b.Property<int>("CaratId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaratId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("CaratId");

                    b.ToTable("Carat");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Clarity", b =>
                {
                    b.Property<int>("ClarityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClarityId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("ClarityId");

                    b.ToTable("Clarity");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("ColorId");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Counter", b =>
                {
                    b.Property<int>("CounterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CounterId"));

                    b.Property<string>("CounterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("CounterId");

                    b.ToTable("Counter");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Cut", b =>
                {
                    b.Property<int>("CutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CutId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("CutId");

                    b.ToTable("Cut");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Gem", b =>
                {
                    b.Property<int>("GemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GemId"));

                    b.Property<int?>("CaratId")
                        .HasColumnType("int");

                    b.Property<int?>("ClarityId")
                        .HasColumnType("int");

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<int?>("CutId")
                        .HasColumnType("int");

                    b.Property<string>("FeaturedImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("OriginId")
                        .HasColumnType("int");

                    b.Property<int?>("ShapeId")
                        .HasColumnType("int");

                    b.HasKey("GemId");

                    b.HasIndex("CaratId");

                    b.HasIndex("ClarityId");

                    b.HasIndex("ColorId");

                    b.HasIndex("CutId");

                    b.HasIndex("OriginId");

                    b.HasIndex("ShapeId");

                    b.ToTable("Gem");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.GemPriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CaratId")
                        .HasColumnType("int");

                    b.Property<int?>("ClarityId")
                        .HasColumnType("int");

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<int?>("CutId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EffDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OriginId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CaratId");

                    b.HasIndex("ClarityId");

                    b.HasIndex("ColorId");

                    b.HasIndex("CutId");

                    b.HasIndex("OriginId");

                    b.ToTable("GemPriceList");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenderId"));

                    b.Property<string>("GenderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenderId");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("InOrOut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("PerDiscount")
                        .HasColumnType("real");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<float>("TotalWithDiscount")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WarrantyId")
                        .HasColumnType("int");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.HasIndex("WarrantyId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.InvoiceDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("InvoiceDetail");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaterialId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.MaterialPriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("BuyPrice")
                        .HasColumnType("real");

                    b.Property<DateTime>("EffDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<float>("SellPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("MaterialPriceList");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Origin", b =>
                {
                    b.Property<int>("OriginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OriginId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("OriginId");

                    b.ToTable("Origin");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CounterId")
                        .HasColumnType("int");

                    b.Property<string>("FeaturedImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GenderId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("PercentPriceRate")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("real");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<float>("ProductionCost")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CounterId");

                    b.HasIndex("GenderId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("UnitId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.ProductGem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GemId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GemId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductGem");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.ProductMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductMaterial");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Shape", b =>
                {
                    b.Property<int>("ShapeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShapeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PriceRate")
                        .HasColumnType("real");

                    b.HasKey("ShapeId");

                    b.ToTable("Shape");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CounterId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("CounterId");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Warranty", b =>
                {
                    b.Property<int>("WarrantyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("WarrantyId");

                    b.ToTable("Warranty");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Gem", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Carat", "Carat")
                        .WithMany("Gems")
                        .HasForeignKey("CaratId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Clarity", "Clarity")
                        .WithMany("Gems")
                        .HasForeignKey("ClarityId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Color", "Color")
                        .WithMany("Gems")
                        .HasForeignKey("ColorId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Cut", "Cut")
                        .WithMany("Gems")
                        .HasForeignKey("CutId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Origin", "Origin")
                        .WithMany("Gems")
                        .HasForeignKey("OriginId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Shape", "Shape")
                        .WithMany("Gems")
                        .HasForeignKey("ShapeId");

                    b.Navigation("Carat");

                    b.Navigation("Clarity");

                    b.Navigation("Color");

                    b.Navigation("Cut");

                    b.Navigation("Origin");

                    b.Navigation("Shape");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.GemPriceList", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Carat", "Carat")
                        .WithMany("GemPrices")
                        .HasForeignKey("CaratId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Clarity", "Clarity")
                        .WithMany("GemPrices")
                        .HasForeignKey("ClarityId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Color", "Color")
                        .WithMany("GemPrices")
                        .HasForeignKey("ColorId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Cut", "Cut")
                        .WithMany("GemPrices")
                        .HasForeignKey("CutId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Origin", "Origin")
                        .WithMany("GemPrices")
                        .HasForeignKey("OriginId");

                    b.Navigation("Carat");

                    b.Navigation("Clarity");

                    b.Navigation("Color");

                    b.Navigation("Cut");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Invoice", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.HasOne("JewelrySalesSystem.DAL.Entities.User", "User")
                        .WithMany("Invoices")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Warranty", "Warranty")
                        .WithMany("Invoices")
                        .HasForeignKey("WarrantyId")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");

                    b.Navigation("Warranty");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.InvoiceDetail", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId")
                        .IsRequired();

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Product", "Product")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.MaterialPriceList", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Material", "Material")
                        .WithMany("MaterialPrices")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Product", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Counter", "Counter")
                        .WithMany("Products")
                        .HasForeignKey("CounterId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Gender", "Gender")
                        .WithMany("Products")
                        .HasForeignKey("GenderId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .IsRequired();

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Unit", "Unit")
                        .WithMany("Products")
                        .HasForeignKey("UnitId");

                    b.Navigation("Category");

                    b.Navigation("Counter");

                    b.Navigation("Gender");

                    b.Navigation("ProductType");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.ProductGem", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Gem", "Gem")
                        .WithMany("ProductGems")
                        .HasForeignKey("GemId")
                        .IsRequired();

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Product", "Product")
                        .WithMany("ProductGems")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Gem");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.ProductMaterial", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Material", "Material")
                        .WithMany("ProductMaterials")
                        .HasForeignKey("MaterialId")
                        .IsRequired();

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Product", "Product")
                        .WithMany("ProductMaterials")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.User", b =>
                {
                    b.HasOne("JewelrySalesSystem.DAL.Entities.Counter", "Counter")
                        .WithMany()
                        .HasForeignKey("CounterId");

                    b.HasOne("JewelrySalesSystem.DAL.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Counter");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Carat", b =>
                {
                    b.Navigation("GemPrices");

                    b.Navigation("Gems");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Clarity", b =>
                {
                    b.Navigation("GemPrices");

                    b.Navigation("Gems");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Color", b =>
                {
                    b.Navigation("GemPrices");

                    b.Navigation("Gems");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Counter", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Customer", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Cut", b =>
                {
                    b.Navigation("GemPrices");

                    b.Navigation("Gems");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Gem", b =>
                {
                    b.Navigation("ProductGems");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Gender", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Material", b =>
                {
                    b.Navigation("MaterialPrices");

                    b.Navigation("ProductMaterials");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Origin", b =>
                {
                    b.Navigation("GemPrices");

                    b.Navigation("Gems");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Product", b =>
                {
                    b.Navigation("InvoiceDetails");

                    b.Navigation("ProductGems");

                    b.Navigation("ProductMaterials");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.ProductType", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Shape", b =>
                {
                    b.Navigation("Gems");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Unit", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.User", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("JewelrySalesSystem.DAL.Entities.Warranty", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
