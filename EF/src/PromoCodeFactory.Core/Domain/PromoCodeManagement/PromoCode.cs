using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class PromoCode : BaseEntity
    {
        public string Code { get; set; }
        public string ServiceInfo { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PartnerName { get; set; }

        public Guid CustomerId { get; set; }                // Внешний ключ для Customer
        public virtual Customer Customer { get; set; }      // Связь с Customer

        public Guid PreferenceId { get; set; }              // Внешний ключ для Preference
        public virtual Preference Preference { get; set; }  // Связь с Preference

        public virtual ICollection<CustomerPreference> CustomerPreferences { get; set; } // Связь с CustomerPreference
    }
}