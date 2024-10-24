using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class CustomerDto {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Required]
        public List<PreferenceDto> Preferences { get; set; }
        [Required]
        public List<PromoCodeShort> PromoCodes { get; set; }
    }
}
