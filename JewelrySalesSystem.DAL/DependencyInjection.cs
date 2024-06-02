using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JewelrySalesSystem.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDALServices(
            this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<JewelryDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
