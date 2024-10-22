using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Services.Implementations;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController : ControllerBase
    {
        private readonly IRepository<PromoCode> _promoCodeRepository;
        private readonly IRepository<Preference> _preferenceRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerPreference> _customerPreferencesRepository;


        public PromocodesController(IRepository<PromoCode> promoCodeRepository,
                                    IRepository<Preference> preferenceRepository,
                                    IRepository<Customer> customerRepository,
                                    IRepository<CustomerPreference> customerPreferencesRepository) {
                                        _promoCodeRepository = promoCodeRepository;
                                        _preferenceRepository = preferenceRepository;
                                        _customerRepository = customerRepository;
                                        _customerPreferencesRepository = customerPreferencesRepository;
        }

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromoСodesAsync() {
            var promoCodes = await _promoCodeRepository.GetAllAsync();

            var response = promoCodes.Select(pc => new PromoCodeShortResponse
            {
                Id = pc.Id,
                Code = pc.Code,
                ServiceInfo = pc.ServiceInfo,
                BeginDate = pc.BeginDate.ToString("o"), // ISO 8601 формат
                EndDate = pc.EndDate.ToString("o"),     // ISO 8601 формат
                PartnerName = pc.PartnerName
            }).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <param name="id">ID предпочтения</param>
        /// <param name="request">Запрос для создания промокода.</param>
        /// <returns>Результат операции.</returns>
        /// 
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync([FromBody] GivePromoCodeRequest request) {
            var customers = await _customerRepository.GetAllAsync();
            var customerPreferences = await _customerPreferencesRepository.GetAllAsync();
            var customerForCode = customerPreferences.Where(p => p.PreferenceId == request.PreferenceId).ToList();
            
            var promoCode = new PromoCode
            {
                Id = Guid.NewGuid(),
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Parse(request.BeginDate),
                EndDate = DateTime.Parse(request.EndDate),
                PartnerName = request.PartnerName,
                PreferenceId = request.PreferenceId
            };

            foreach(var c in customers) {
                foreach (var preference in customerForCode) {
                    if (c.Id == preference.CustomerId) {
                        promoCode.CustomerId = c.Id;
                        promoCode.PreferenceId = preference.PreferenceId;
                        await _promoCodeRepository.CreateAsync(promoCode);
                    }
                }
            }
            return Ok();
        }





        //public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        //{
        //    // Шаг 1: Проверяем, существует ли указанный PreferenceId
        //    var preferenceExists = await _preferenceRepository.GetByIdAsync(request.PreferenceId);
        //    if (preferenceExists == null)
        //        return BadRequest($"Preference with Id {request.PreferenceId} does not exist.");

        //    // Шаг 2: Создаем новый промокод на основе переданных данных
        //    var promoCode = new PromoCode
        //    {
        //        Id = Guid.NewGuid(),
        //        Code = request.PromoCode,
        //        ServiceInfo = request.ServiceInfo,
        //        BeginDate = DateTime.Parse(request.BeginDate),
        //        EndDate = DateTime.Parse(request.EndDate),
        //        PartnerName = request.PartnerName,
        //        PreferenceId = request.PreferenceId
        //    };

        //    // Шаг 3: Сохраняем новый промокод в базе данных
        //    await _promoCodeRepository.CreateAsync(promoCode);

        //    //// Удаление промокодов, выданных клиенту:
        //    //var promoCodes = await _promoCodeRepository.GetAllAsync();
        //    //foreach (var promoCode in promoCodes.Where(pc => pc.CustomerId == id))
        //    //{
        //    //    await _promoCodeRepository.DeleteAsync(promoCode.Id);
        //    //}

        //    // Шаг 4: Находим всех клиентов, у которых есть указанное предпочтение
        //    var customersWithPreference = await _customerPreferencesRepository.GetAllAsync();
        //    var customersToGivePromo = customersWithPreference
        //        .Where(p => p.PreferenceId == request.PreferenceId)
        //        .ToList();

        //    if (!customersToGivePromo.Any())
        //        return NotFound("No customers found with the specified preference.");

        //    // Теперь найдем клиентов, которым нужно выдать промокод
        //    var customerIdsWithPreference = customersToGivePromo.Select(p => p.CustomerId).ToList();

        //    // Получаем список всех клиентов из репозитория
        //    var customers = await _customerRepository.GetAllAsync();

        //    // Фильтруем клиентов по тем, которые имеют указанное предпочтение
        //    foreach (var customer in customers.Where(c => customerIdsWithPreference.Contains(c.Id)))
        //    {
        //        // Добавляем промокод в список промокодов клиента
        //        customer.PromoCodes.Add(promoCode);

        //        // Обновляем клиента в базе данных
        //        await _customerRepository.UpdateAsync(customer);
        //    }

        //    //// Шаг 5: Добавляем новый промокод каждому клиенту с указанным предпочтением
        //    //foreach (var customer in customersToGivePromo)
        //    //    {
        //    //        // Добавляем промокод в список промокодов клиента
        //    //        customer.PromoCodes.Add(promoCode);

        //    //        // Обновляем клиента с новыми данными
        //    //        await _customerRepository.UpdateAsync(customer);
        //    //    }

        //    return Ok();
        //}
    }
}



    //[HttpPost]
    //public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync( GivePromoCodeRequest request)
    //{
    //    // Создаем новый промокод:
    //    var promoCode = new PromoCode
    //    {
    //        Id = Guid.NewGuid(),
    //        Code = request.PromoCode,
    //        ServiceInfo = request.ServiceInfo,
    //        BeginDate = DateTime.Parse(request.BeginDate),
    //        EndDate = DateTime.Parse(request.EndDate),
    //        PartnerName = request.PartnerName
    //    };

    //    // Сохраняем промокод в базе данных:
    //    await _promoCodeRepository.CreateAsync(promoCode);

    //    // Находим клиентов с указанным предпочтением:
    //    var customersWithPreference = await _customerRepository.GetAllAsync();
    //    var customersToGivePromo = customersWithPreference
    //        .Where(c => c.CustomerPreferences.Any(cp => cp.PreferenceId == request.PreferenceId))
    //        .ToList();

    //    // Выдаем промокод всем клиентам с указанным предпочтением:
    //    foreach (var customerWithCode in customersToGivePromo)
    //    {
    //        // Создаем новую связь между клиентом и промокодом:
    //        var newPromoCode = new PromoCode
    //        {
    //            Id = promoCode.Id,                          // Используем уже существующий ID промокода
    //            Code = promoCode.Code,
    //            ServiceInfo = promoCode.ServiceInfo,
    //            BeginDate = promoCode.BeginDate,
    //            EndDate = promoCode.EndDate,
    //            PartnerName = promoCode.PartnerName
    //        };

    //        customerWithCode.PromoCodes.Add(newPromoCode);
    //    }

    //    // Обновляем всех клиентов в базе данных:
    //    foreach (var customerWithCode in customersToGivePromo) {
    //        await _customerRepository.UpdateAsync(customerWithCode);
    //    }

    //    return CreatedAtAction(nameof(GetPromocodesAsync), new { id = promoCode.Id }, promoCode);
    //}


