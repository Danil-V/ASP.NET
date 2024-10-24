using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class CreateOrEditCustomerDto {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Required]
        public List<Guid> PreferenceIds { get; set; }
    }
}
