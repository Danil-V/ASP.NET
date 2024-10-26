using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController : ControllerBase {
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
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromoСodesAsync(CancellationToken cancellationToken) {
            var promoCodes = await _promoCodeRepository.GetAllAsync(cancellationToken);

            var response = promoCodes.Select(pc => new PromoCodeShortResponse {
                Id = pc.Id,
                Code = pc.Code,
                ServiceInfo = pc.ServiceInfo,
                BeginDate = pc.BeginDate.ToString("yyyy-MM-dd"), // ISO 8601 формат
                EndDate = pc.EndDate.ToString("yyyy-MM-dd"),     // ISO 8601 формат
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
        /// <param name="request">Запрос для создания промокода.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Результат операции.</returns>
        /// 
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request, CancellationToken cancellationToken) {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);
            var customerPreferences = await _customerPreferencesRepository.GetAllAsync(cancellationToken);
            var customerPreferenceForCode = customerPreferences.Where(p => p.PreferenceId == request.PreferenceId).ToList();


            foreach (var c in customers) {
                foreach (var preference in customerPreferenceForCode) {
                    if (c.Id == preference.CustomerId) {
                        var promoCode = new PromoCode {
                            Id = Guid.NewGuid(),
                            Code = request.PromoCode,
                            ServiceInfo = request.ServiceInfo,
                            BeginDate = DateTime.Parse(request.BeginDate),
                            EndDate = DateTime.Parse(request.EndDate),
                            PartnerName = request.PartnerName,
                            CustomerId = c.Id,
                            PreferenceId = preference.PreferenceId
                        };
                        await _promoCodeRepository.CreateAsync(promoCode, cancellationToken);
                    }
                }
            }
            return Ok();
        }
    }
}




