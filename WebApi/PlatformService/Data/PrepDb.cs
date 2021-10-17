using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPoulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("******  Seeding data...");

                context.AddRange(
                    new Platform()
                    {
                        Name = "PlatformName1",
                        Publisher = "Publisher1",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "PlatformName2",
                        Publisher = "Publisher2",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "PlatformName3",
                        Publisher = "Publisher3",
                        Cost = "Free"
                    });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("******  We already have data!");
            }
        }
    }
}
