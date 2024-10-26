﻿using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System.Linq;

namespace PromoCodeFactory.Core.DataAccess.EntityFramework
{
    public class DatabaseContext : DbContext {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPreference> CustomerPreferences { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }   // Конструктор для работы с БД
        public DatabaseContext() : base() { }   // Конструктор по умолчанию для миграций


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Исключение FullName из маппинга (для миграции)
            modelBuilder.Entity<Customer>().Ignore(c => c.FullName);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)            // Указывает, что Employee имеет одну Role
                .WithMany(r => r.Employees)     // Указывает, что Role может иметь много Employees
                .HasForeignKey(e => e.RoleId)   // Указывает, что RoleId - это внешний ключ
                .IsRequired();                  // Указывает, что роль обязательна для каждого сотрудника
                                                //.OnDelete(DeleteBehavior.Restrict); // Используем при миграции - это свойство предотвратит удаление связанных данных при миграции

            modelBuilder.Entity<PromoCode>()
                .HasOne(e => e.Preference)
                .WithMany(r => r.PromoCodes)
                .HasForeignKey(e => e.PreferenceId);
                //.OnDelete(DeleteBehavior.Cascade);

            // Настройка маппинга для связи Customer и Preference (Many-to-Many)
            modelBuilder.Entity<CustomerPreference>()
                .HasKey(cp => new { cp.CustomerId, cp.PreferenceId });
            modelBuilder.Entity<CustomerPreference>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.CustomerPreferences)
                .HasForeignKey(cp => cp.CustomerId);
                //.OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CustomerPreference>()
                .HasOne(cp => cp.Preference)
                .WithMany(p => p.CustomerPreferences)
                .HasForeignKey(cp => cp.PreferenceId);
                //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.FullName).HasMaxLength(200);
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(200);

            modelBuilder.Entity<Preference>().Property(p => p.Name).HasMaxLength(100);

            modelBuilder.Entity<PromoCode>().Property(pc => pc.Code).HasMaxLength(250);
            modelBuilder.Entity<PromoCode>().Property(pc => pc.ServiceInfo).HasMaxLength(200);
            modelBuilder.Entity<PromoCode>().Property(pc => pc.PartnerName).HasMaxLength(200);
        }

        // Настройка провайдера базы данных для миграций
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Укажите здесь провайдер базы данных, например, SQLite:
                optionsBuilder.UseSqlite("Data Source=PromoCodeFactory.db");
            }
        }
    }
}