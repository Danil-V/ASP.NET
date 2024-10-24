using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerService
{
    public class CustomerShortEdited {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Required]
        public List<Preference> Preferences { get; set; }
    }
}
