using System;
using System.Collections.Generic;
using System.Linq;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace PromoCodeFactory.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static IList<Employee> Employees => new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                RoleId = Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                RoleId = Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id,
                AppliedPromocodesCount = 10
            }
        };

        public static IList<Role> Roles => new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Name = "Admin",
                Description = "Администратор",
            },
            new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер"
            }
        };

        public static IList<Preference> Preferences => new List<Preference>()
        {
            new Preference()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Preference()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Preference()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        public static IList<PromoCode> PromoCodes => new List<PromoCode>()
        {
            new PromoCode()
            {
                Id = Guid.Parse("53729346-a368-4eeb-8bfa-cc69b6050d21"),
                Code = "EASY PEASY -20% OFF",
                ServiceInfo = "Скидка для сотрудников",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(14),
                PartnerName = "Иван Петров",
                PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                CustomerPreferences = new List<CustomerPreference>().Select(c => new CustomerPreference()
                {
                    CustomerId = c.CustomerId,
                    PreferenceId = c.PreferenceId
                }).ToList(),
                CustomerId = Guid.Parse("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f")
            }
        };

        public static IList<Customer> Customers => new List<Customer>()
        {
            new Customer()
            {
                Id = Guid.Parse("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"),
                Email = "ivan_sergeev@mail.ru",
                FirstName = "Иван",
                LastName = "Петров",
                CustomerPreferences = new List<CustomerPreference>().Select(c => new CustomerPreference()
                {
                    CustomerId = c.CustomerId,
                    PreferenceId = c.PreferenceId
                }).ToList()
            }
        };

        public static List<CustomerPreference> CustomerPreferences => new List<CustomerPreference>
        {
            new CustomerPreference
            {
                Id = Guid.NewGuid(),
                CustomerId = Customers[0].Id,
                PreferenceId = Preferences[0].Id
            },
            new CustomerPreference() {
                Id = Guid.NewGuid(),
                CustomerId = Customers[0].Id,
                PreferenceId = Preferences[1].Id
            },
            new CustomerPreference
            {
                Id = Guid.NewGuid(),
                CustomerId = Customers[0].Id,
                PreferenceId = Preferences[2].Id
            }
        };
    }
}