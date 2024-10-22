using System;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class CustomerShort
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
