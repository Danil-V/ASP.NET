using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<PreferenceDto> Preferences { get; set; }
        public List<PromoCodeShort> PromoCodes { get; set; }
    }
}
