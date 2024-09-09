using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Sevices.Interfaces;
using PromoCodeFactory.WebHost.Models;
using System.Data;

namespace PromoCodeFactory.Sevices.Sevices
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository) {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> EmployeeGetDtoAsync(Guid id, EmployeeRequest employeeWebData)
        {
            List<Role> roles = new List<Role>();
            var employee = new Employee();
            var role = new Role
            {
                Name = employeeWebData.RoleName,
                Description = employeeWebData.RoleDescription
            };
            roles.Add(role);

            if (employeeWebData != null)
            {
                employee.Id = id;
                employee.FirstName = employeeWebData.FirstName;
                employee.LastName = employeeWebData.LastName;
                employee.Email = employeeWebData.Email;
                employee.Roles = roles.Select(x => new Role()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList();
            }
            roles.Clear();

            return await Task.FromResult(employee);
        }
    }
}
