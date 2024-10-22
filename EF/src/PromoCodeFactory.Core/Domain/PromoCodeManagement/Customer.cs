using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Customer : BaseEntity
    {
        private string _fullName;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName // Для миграции требовался приватный сеттер 
        {
            get => _fullName ??= $"{FirstName} {LastName}";     // Ленивая инициализация
            private set => _fullName = value;                   // Для возможности установить значение при необходимости
        }
        // public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public int AppliedPromocodesCount { get; set; }         // Свойство для доп задания

        public virtual ICollection<Preference> Preferences { get; set; } // Связь с Preference
        public virtual ICollection<PromoCode> PromoCodes { get; set; }  // Связь с PromoCode
        public virtual ICollection<CustomerPreference> CustomerPreferences { get; set; } // Связь с CustomerPreference
    }
}