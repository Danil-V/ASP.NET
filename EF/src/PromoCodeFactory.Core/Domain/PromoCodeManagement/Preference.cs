using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Preference : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<PromoCode> PromoCodes { get; set; }                      // Связь с PromoCode
        public virtual ICollection<CustomerPreference> CustomerPreferences { get; set; }    // Связь с CustomerPreference
    }
}