using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;


namespace PromoCodeFactory.Sevices.Interfaces
{
    public interface IEmployeeService {
        Task<Employee> EmployeeCreateDtoAsync(EmployeeRequest updateEmployeeData);
        Task<Employee> EmployeeUpdateDtoAsync(Guid id, EmployeeRequest updateEmployeeData);
    }
}
