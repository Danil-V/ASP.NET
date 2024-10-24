using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerService;
using PromoCodeFactory.Services.Interfaces;

namespace PromoCodeFactory.Services.Implementations
{
    public class CustomerService : ICustomerService {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerPreference> _customerPreferenceRepository;
        private readonly IRepository<PromoCode> _promoCodeRepository;
        private readonly IRepository<Preference> _preferenceRepository;

        public CustomerService(
            IRepository<Preference> preferenceRepository,
            IRepository<PromoCode> promoCodeRepository,
            IRepository<Customer> customerRepository,
            IRepository<CustomerPreference> customerPreferenceRepository) {
                _preferenceRepository = preferenceRepository;
                _customerRepository = customerRepository;
                _customerPreferenceRepository = customerPreferenceRepository;
                _promoCodeRepository = promoCodeRepository;
        }


        public async Task<IEnumerable<CustomerShort>> GetAllCustomersAsync(CancellationToken cancellationToken) {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                return (IEnumerable<CustomerShort>)customers;

                var customersList = customers.Select(x =>
                    new CustomerShort()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email
                    }).ToList();
                return customersList;
        }


        public async Task<CustomerDto> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken) {
            var promoCodes = new List<PromoCode>();
            var codes = await _promoCodeRepository.GetAllAsync(cancellationToken); // Вызов из-за Lazy работы EF (без нее не работает, так как нет обращения к данному репозиторию)
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken, c => c.CustomerPreferences);
            if (customer == null)
                return null;

            // Загрузка промокодов через предпочтения:
            foreach (var customerPreference in customer.CustomerPreferences) {
                var preferenceWithPromoCodes = await _preferenceRepository.GetByIdAsync(customerPreference.PreferenceId, cancellationToken);

                if (preferenceWithPromoCodes != null && preferenceWithPromoCodes.PromoCodes != null)
                    promoCodes.AddRange(preferenceWithPromoCodes.PromoCodes);
            }

            var response = new CustomerDto {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Preferences = customer.CustomerPreferences.Select(cp => new PreferenceDto {
                    Id = cp.Preference.Id,
                    Name = cp.Preference.Name
                }).ToList(),
                PromoCodes = promoCodes.Select(pc => new PromoCodeShort {
                    Id = pc.Id,
                    Code = pc.Code,
                    ServiceInfo = pc.ServiceInfo,
                    BeginDate = pc.BeginDate.ToString(("yyyy-MM-dd")),
                    EndDate = pc.EndDate.ToString(("yyyy-MM-dd")),
                    PartnerName = pc.PartnerName
                }).ToList()
            };
            return response;
        }


        public async Task<Customer> CreateCustomerAsync(CreateOrEditCustomerDto request, CancellationToken cancellationToken) {
            var customer = new Customer {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            await _customerRepository.CreateAsync(customer, cancellationToken);

            // Если указаны предпочтения, связываем их с клиентом через CustomerPreference:
            if (request.PreferenceIds != null && request.PreferenceIds.Any()) {
                var preferences = await _preferenceRepository.GetAllAsync(cancellationToken);
                var selectedPreferences = preferences
                    .Where(p => request.PreferenceIds.Contains(p.Id))
                    .ToList();

                if (selectedPreferences.Any()) {
                    // Создаем связи между клиентом и предпочтениями:
                    foreach (var preference in selectedPreferences) {
                        var customerPreference = new CustomerPreference {
                            Id = Guid.NewGuid(),
                            CustomerId = customer.Id,
                            PreferenceId = preference.Id
                        };
                        await _customerPreferenceRepository.CreateAsync(customerPreference, cancellationToken);
                    }
                }
            }
            return customer;
        }


        public async Task<CustomerShortEdited> EditCustomerAsync(Guid id, CreateOrEditCustomerDto request, CancellationToken cancellationToken) {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
            if (customer == null)
                return null;

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            await _customerRepository.UpdateAsync(customer, cancellationToken);

            // Обработка предпочтений клиента
            await UpdateCustomerPreferences(customer, request.PreferenceIds, cancellationToken);

            return new CustomerShortEdited {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Preferences = customer.CustomerPreferences.Select(cp => new Preference {
                    Id = cp.Preference.Id,
                    Name = cp.Preference.Name
                }).ToList()
            };
        }


        public async Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken) {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
            if (customer == null)
                return false;

            // Удаление промокодов, выданных клиенту:
            var promoCodes = await _promoCodeRepository.GetAllAsync(cancellationToken);
            foreach (var promoCode in promoCodes.Where(pc => pc.CustomerId == id)) {
                await _promoCodeRepository.DeleteAsync(promoCode.Id, cancellationToken);
            }

            await _customerRepository.DeleteAsync(id, cancellationToken);
            return true;
        }


        private async Task UpdateCustomerPreferences(Customer customer, List<Guid> preferenceIds, CancellationToken cancellationToken) {
            var customerPreferences = await _customerPreferenceRepository.GetAllAsync(cancellationToken);
            foreach (var customerPreference in customerPreferences.Where(pc => pc.CustomerId == customer.Id)) {
                await _customerPreferenceRepository.DeleteAsync(customerPreference.Id, cancellationToken);
            }

            // Получаем новые предпочтения:
            var preferences = await _preferenceRepository.GetAllAsync(cancellationToken);
            var selectedPreferences = preferences
                .Where(p => preferenceIds.Contains(p.Id))   // Проверяем, содержится ли значение p.Id в коллекции preferenceIds
                .ToList();

            // Проверяем, существует ли хотя бы один элемент в коллекции:
            if (selectedPreferences.Any()) {
                // Создаем связи между клиентом и предпочтениями:
                foreach (var preference in selectedPreferences) {
                    var customerPreference = new CustomerPreference {
                        Id = Guid.NewGuid(),
                        CustomerId = customer.Id,
                        PreferenceId = preference.Id
                    };
                    await _customerPreferenceRepository.CreateAsync(customerPreference, cancellationToken);
                }
            }
        }
    }
}

