using PromoCodeFactory.Core.DataAccess.EntityFramework;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System.Linq;

namespace PromoCodeFactory.DataAccess.Data
{
    public static class DatabaseContextExtensions
    {
        public static void SeedData(this DatabaseContext db) {  //Заполнение тестовыми данными:
            var roles = FakeDataFactory.Roles
                .Select(r => new Role {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToArray();
            db.Roles.AddRange(roles);

            var employees = FakeDataFactory.Employees
                .Select(e => new Employee {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    RoleId = e.RoleId,
                    AppliedPromocodesCount = e.AppliedPromocodesCount
                }).ToArray();
            db.Employees.AddRange(employees);

            var preferences = FakeDataFactory.Preferences
            .Select(p => new Preference {
                Id = p.Id,
                Name = p.Name
            }).ToArray();
            db.Preferences.AddRange(preferences);

            var promoCodes = FakeDataFactory.PromoCodes
               .Select(pc => new PromoCode {
                   Id = pc.Id,
                   Code = pc.Code,
                   ServiceInfo = pc.ServiceInfo,
                   BeginDate = pc.BeginDate,
                   EndDate = pc.EndDate,
                   CustomerPreferences = pc.CustomerPreferences,
                   PreferenceId = pc.PreferenceId,
                   PartnerName = pc.PartnerName,
                   CustomerId = pc.CustomerId
               }).ToArray();
            db.PromoCodes.AddRange(promoCodes);

            var customers = FakeDataFactory.Customers
                .Select(c => new Customer {
                    Id = c.Id,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }).ToArray();
            db.Customers.AddRange(customers);

            var customerPreferences = FakeDataFactory.CustomerPreferences
                .Select(cp => new CustomerPreference {
                    Id = cp.Id,
                    CustomerId = cp.CustomerId,
                    PreferenceId = cp.PreferenceId
                }).ToArray();
            db.CustomerPreferences.AddRange(customerPreferences);

            db.SaveChanges();
        }
    }
}
