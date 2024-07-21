using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any()){
                Console.WriteLine("--> Seeding data");

                context.Platforms.AddRange(
                    new Platform(){
                        Name = "Dotnet",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    
                    new Platform(){
                        Name = "SqlServer",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    
                    new Platform(){
                        Name = "Kubernetes",
                        Publisher = "CLoud Native Computing Foundations",
                        Cost = "Free"
                    }
                );
            }
            else{
                Console.WriteLine("--> we already have data");
            }
            context.SaveChanges();
        }
    }
}
