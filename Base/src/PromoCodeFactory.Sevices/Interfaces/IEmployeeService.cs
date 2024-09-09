using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;


namespace PromoCodeFactory.Sevices.Interfaces
{
    public interface IEmployeeService {
        Task<Employee> EmployeeGetDtoAsync(Guid employeeId, EmployeeRequest newEmployeeData);
    }
}
