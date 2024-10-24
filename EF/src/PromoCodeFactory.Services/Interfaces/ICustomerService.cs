using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerService;

namespace PromoCodeFactory.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerShort>> GetAllCustomersAsync(CancellationToken cancellationToken);

        Task<CustomerDto> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Customer> CreateCustomerAsync(CreateOrEditCustomerDto request, CancellationToken cancellationToken);
        Task<CustomerShortEdited> EditCustomerAsync(Guid id, CreateOrEditCustomerDto request, CancellationToken cancellationToken);
        Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
    }
}
