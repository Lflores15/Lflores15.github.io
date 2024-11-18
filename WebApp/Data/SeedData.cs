using Microsoft.Extensions.DependencyInjection;
using WebApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, WebAppDbContext context)
        {
            if (!context.Countries.Any())
            {
                context.Countries.AddRange(
                    new Country { Name = "United States" },
                    new Country { Name = "Canada" },
                    new Country { Name = "United Kingdom" },
                    new Country { Name = "Australia" },
                    new Country { Name = "Germany" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}


