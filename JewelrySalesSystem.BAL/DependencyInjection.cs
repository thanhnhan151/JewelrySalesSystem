using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JewelrySalesSystem.BAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureBALServices(this IServiceCollection services
            /*, IConfiguration configuration*/)
        {           
            services.AddScoped<IWarrantyService, WarrantyService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IMaterialService, MaterialService>();

            services.AddScoped<IInvoiceService, InvoiceService>();

            services.AddScoped<IGemService, GemService>();

            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
