using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JewelrySalesSystem.DAL
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureDALServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<JewelryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
