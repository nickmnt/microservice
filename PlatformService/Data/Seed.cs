using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class Seed
    {
        public static void PreparePopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform
                    {
                        Name="Dot Net",
                        Publisher = "Microsoft",
                        Cost= "Free"
                    },
                    new Platform
                    {
                        Name="SQL Server Express",
                        Publisher = "Microsoft",
                        Cost= "Free"
                    }, new Platform
                    {
                        Name="Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost= "Free"
                    });

                context.SaveChanges();
            }
        }
    }
}