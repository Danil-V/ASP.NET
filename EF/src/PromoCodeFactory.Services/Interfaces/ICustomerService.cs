using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerService;

namespace PromoCodeFactory.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerShort>> GetAllCustomersAsync();

        Task<CustomerDto> GetCustomerByIdAsync(Guid id);
        Task<Customer> CreateCustomerAsync(CreateOrEditCustomerDto request);
        Task<CustomerShortEdited> EditCustomerAsync(Guid id, CreateOrEditCustomerDto request);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
