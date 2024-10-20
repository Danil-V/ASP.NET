using PromoCodeFactory.Core.Domain.Administration;


namespace PromoCodeFactory.Sevices.Interfaces
{
    public interface IEmployeeService {
        Task<Employee> EmployeeGetDtoAsync(Guid employeeId, ShortEmployee newEmployeeData);
    }
}
