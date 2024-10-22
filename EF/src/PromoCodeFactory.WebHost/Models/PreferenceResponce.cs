using System;

namespace PromoCodeFactory.WebHost.Models
{
    public class PreferenceResponse
    {
        /// <summary>
        /// Уникальный идентификатор предпочтения.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название предпочтения.
        /// </summary>
        public string Name { get; set; }
    }
    
}
