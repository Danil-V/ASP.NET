using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromoCodeFactory.Core.DataAccess.EntityFramework;
using PromoCodeFactory.DataAccess.Data;
using System;

namespace PromoCodeFactory.WebHost
{
    public class Program
    {
        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
             
                try {
                    db.Database.Migrate();
                } catch (Exception ex) { Console.WriteLine($"Error during migration: {ex.Message}"); }
                
                db.SeedData();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}