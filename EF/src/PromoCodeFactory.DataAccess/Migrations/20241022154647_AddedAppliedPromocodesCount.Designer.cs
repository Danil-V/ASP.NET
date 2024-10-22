﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromoCodeFactory.Core.DataAccess.EntityFramework;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241022154647_AddedAppliedPromocodesCount")]
    partial class AddedAppliedPromocodesCount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AppliedPromocodesCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                            AppliedPromocodesCount = 5,
                            Email = "owner@somemail.ru",
                            FirstName = "Иван",
                            LastName = "Сергеев",
                            RoleId = new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02")
                        },
                        new
                        {
                            Id = new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"),
                            AppliedPromocodesCount = 10,
                            Email = "andreev@somemail.ru",
                            FirstName = "Петр",
                            LastName = "Андреев",
                            RoleId = new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665")
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                            Description = "Администратор",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                            Description = "Партнерский менеджер",
                            Name = "PartnerManager"
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AppliedPromocodesCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"),
                            AppliedPromocodesCount = 0,
                            Email = "ivan_sergeev@mail.ru",
                            FirstName = "Иван",
                            LastName = "Петров"
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PromoCodeId")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId", "PreferenceId");

                    b.HasIndex("PreferenceId");

                    b.HasIndex("PromoCodeId");

                    b.ToTable("CustomerPreferences");

                    b.HasData(
                        new
                        {
                            CustomerId = new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"),
                            PreferenceId = new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                            Id = new Guid("4a5cf896-c8f3-4a87-bf77-03832dad25aa")
                        },
                        new
                        {
                            CustomerId = new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"),
                            PreferenceId = new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                            Id = new Guid("f84d36a9-c168-447f-9c61-1880e681ebc4")
                        },
                        new
                        {
                            CustomerId = new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"),
                            PreferenceId = new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                            Id = new Guid("dc80c6f2-f4c1-4ede-b1be-34ca40abc140")
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Preferences");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                            Name = "Театр"
                        },
                        new
                        {
                            Id = new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                            Name = "Семья"
                        },
                        new
                        {
                            Id = new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                            Name = "Дети"
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PartnerName")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceInfo")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("PromoCodes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("53729346-a368-4eeb-8bfa-cc69b6050d21"),
                            BeginDate = new DateTime(2024, 10, 22, 18, 46, 47, 473, DateTimeKind.Local).AddTicks(9791),
                            Code = "EASY PEASY -20% OFF",
                            CustomerId = new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"),
                            EndDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            PartnerName = "Иван Петров",
                            PreferenceId = new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                            ServiceInfo = "Скидка для сотрудников"
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.Administration.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", "Customer")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", null)
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("PromoCodeId");

                    b.Navigation("Customer");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", null)
                        .WithMany("Preferences")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", "Customer")
                        .WithMany("PromoCodes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany("PromoCodes")
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Navigation("CustomerPreferences");

                    b.Navigation("Preferences");

                    b.Navigation("PromoCodes");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Navigation("CustomerPreferences");

                    b.Navigation("PromoCodes");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.Navigation("CustomerPreferences");
                });
#pragma warning restore 612, 618
        }
    }
}
