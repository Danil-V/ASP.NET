using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Services.Implementations;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Threading;
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
        public async Task<ActionResult<List<CustomerShortResponse>>> GetCustomersAsync(CancellationToken cancellationToken) {
            try {
                var customersList = await _customerService.GetAllCustomersAsync(cancellationToken);
                return Ok(customersList);
            }
            catch (OperationCanceledException) {
                // Обрабатываем CancellationToken:
                return StatusCode(499, "Client Closed Request");
            }
            catch (Exception ex) {
                // Обрабатываем другие ошибки:
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Получение клиента по ID
        /// </summary>
        /// <param name = "id" > ID клиента</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Детали клиента</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id, CancellationToken cancellationToken) {
            try {
                var response = await _customerService.GetCustomerByIdAsync(id, cancellationToken);
                if (response == null)
                    return NotFound();
                else
                    return Ok(response);
            }
            catch (OperationCanceledException) {
                // Обрабатываем CancellationToken:
                return StatusCode(499, "Client Closed Request");
            }
            catch (Exception ex) {
                // Обрабатываем другие ошибки:
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Создание нового клиента
        /// </summary>
        /// <param name="request">Данные клиента</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Результат создания клиента</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request, CancellationToken cancellationToken) {
            try {
                var data = new CreateOrEditCustomerDto()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PreferenceIds = request.PreferenceIds
                };
                var customer = await _customerService.CreateCustomerAsync(data, cancellationToken);
                return Created($"api/customers/{customer.Id}", customer);
            }
            catch (OperationCanceledException) {
                // Обрабатываем CancellationToken:
                return StatusCode(499, "Client Closed Request");
            }
            catch (Exception ex) {
                // Обрабатываем другие ошибки:
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Обновление данных клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <param name="request">Данные клиента</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Результат обновления клиента</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, 
                                                            CreateOrEditCustomerRequest request, 
                                                            CancellationToken cancellationToken) {
            try {
                // Проверяем, что переданные данные валидны
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = new CreateOrEditCustomerDto()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PreferenceIds = request.PreferenceIds
                };
                // Вызываем сервис для обновления данных клиента
                var editedCustomer = await _customerService.EditCustomerAsync(id, data, cancellationToken);
                if (editedCustomer == null)
                    return NotFound($"Customer with ID {id} not found.");

                return Created($"api/customers/{editedCustomer.Id}", editedCustomer);
            }
            catch (OperationCanceledException) {
                // Обрабатываем CancellationToken:
                return StatusCode(499, "Client Closed Request");
            }
            catch (Exception ex) {
                // Обрабатываем другие ошибки:
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Результат удаления клиента</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(Guid id, CancellationToken cancellationToken) {
            try {
                var result = await _customerService.DeleteCustomerAsync(id, cancellationToken);
                if (result == false)
                    return NotFound();
                else
                    return Ok();
            }
            catch (OperationCanceledException) {
                // Обрабатываем CancellationToken:
                return StatusCode(499, "Client Closed Request");
            }
            catch (Exception ex) {
                // Обрабатываем другие ошибки:
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}