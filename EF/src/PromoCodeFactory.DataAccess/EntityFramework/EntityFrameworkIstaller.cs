using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.Core.DataAccess.EntityFramework;

namespace PromoCodeFactory.DataAccess.EntityFramework
{
    public static class EntityFrameworkIstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder => optionsBuilder
                    .UseSqlite(connectionString));
            return services;
        }
    }
}
