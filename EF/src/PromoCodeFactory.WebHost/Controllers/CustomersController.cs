using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Services.Implementations;
using PromoCodeFactory.Services.Interfaces;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(
            IRepository<Preference> preferenceRepository,
            IRepository<PromoCode> promoCodeRepository,
            IRepository<Customer> customerRepository,
            IRepository<CustomerPreference> customerPreferenceRepository) {
                _customerService = new CustomerService(preferenceRepository,
                                                        promoCodeRepository, 
                                                        customerRepository, 
                                                        customerPreferenceRepository);
        }

        /// <summary>
        /// Получение списка клиентов
        /// </summary>
        /// <returns>Список клиентов</returns>
        [HttpGet]
        public async Task<ActionResult<List<CustomerShortResponse>>> GetCustomersAsync() {
            var customersList = await _customerService.GetAllCustomersAsync();
            return Ok(customersList);
        }

        /// <summary>
        /// Получение клиента по ID
        /// </summary>
        /// <param name = "id" > ID клиента</param>
        /// <returns>Детали клиента</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id) {
            var response = await _customerService.GetCustomerByIdAsync(id);
            if (response == null)
                return NotFound();
            else
                return Ok(response);
        }

        /// <summary>
        /// Создание нового клиента
        /// </summary>
        /// <param name="request">Данные клиента</param>
        /// <returns>Результат создания клиента</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request) {
            var data = new CreateOrEditCustomerDto() {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PreferenceIds = request.PreferenceIds
             };            
            var customer = await _customerService.CreateCustomerAsync(data);
            return Created($"api/customers/{customer.Id}", customer);
        }


        /// <summary>
        /// Обновление данных клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <param name="request">Данные клиента</param>
        /// <returns>Результат обновления клиента</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request) {
            // Проверяем, что переданные данные валидны
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = new CreateOrEditCustomerDto() {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PreferenceIds = request.PreferenceIds
            };
            // Вызываем сервис для обновления данных клиента
            var editedCustomer = await _customerService.EditCustomerAsync(id, data);
            if (editedCustomer == null)
                return NotFound($"Customer with ID {id} not found.");

            return Created($"api/customers/{editedCustomer.Id}", editedCustomer);
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <returns>Результат удаления клиента</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(Guid id) {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (result == false)
                return NotFound();
            else
                return Ok();
        }
    }
}