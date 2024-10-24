using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.DataAccess.EntityFramework;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.DataAccess.Repositories;


namespace PromoCodeFactory.WebHost
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Чтение строки подключения из appsettings.json:
            var connectionString = Configuration.GetConnectionString("SQLite");
            // Настройка контекста для базы данных:
            services.AddDbContext<DatabaseContext>(optionsBuilder => optionsBuilder
                   .UseSqlite(connectionString));

            services.AddControllers();
            // Для сервиса работы с Customer (CreateCustomer)
            services.AddControllers().AddJsonOptions(options =>
            {
                // Указываем сериализатору использовать обработку циклов с помощью ReferenceHandler.Preserve, который позволяет сериализовать циклы объектов.
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true; // Необязательно: для форматирования JSON с отступами
            });

            services.AddScoped(typeof(IRepository<Employee>), (x) =>
                new InMemoryRepository<Employee>(FakeDataFactory.Employees));
            services.AddScoped(typeof(IRepository<Role>), (x) =>
                new InMemoryRepository<Role>(FakeDataFactory.Roles));
            services.AddScoped(typeof(IRepository<Preference>), (x) =>
                new InMemoryRepository<Preference>(FakeDataFactory.Preferences));
            services.AddScoped(typeof(IRepository<Customer>), (x) =>
                new InMemoryRepository<Customer>(FakeDataFactory.Customers));


            services.AddScoped<IRepository<PromoCode>, EfRepository<PromoCode>>();
            services.AddScoped<IRepository<Preference>, EfRepository<Preference>>();
            services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();
            services.AddScoped<IRepository<CustomerPreference>, EfRepository<CustomerPreference>>();

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi(x =>
            {
                x.DocExpansion = "list";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}