using System;

namespace PromoCodeFactory.WebHost.Models
{
    public class GivePromoCodeRequest
    {
        public string ServiceInfo { get; set; }

        public string PartnerName { get; set; }

        public string PromoCode { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public Guid PreferenceId { get; set; }
    }
}