using FluentValidation;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Mappings;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Services;
using JewelrySalesSystem.BAL.Validators.User;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace JewelrySalesSystem.BAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureBALServices(
            this IServiceCollection services)
        {
            services.AddScoped<IWarrantyService, WarrantyService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IMaterialService, MaterialService>();

            services.AddScoped<IInvoiceService, InvoiceService>();

            services.AddScoped<IGemService, GemService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IGenderService, GenderService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IMaterialPriceListService, MaterialPriceListService>();

            services.AddScoped<IProductTypeService, ProductTypeService>();

            services.AddScoped<IColourService, ColourService>();

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IBuyInvoiceService, BuyInvoiceService>();

            services.AddTransient<IValidator<CreateUserRequest>, CreateUserValidator>();
            return services;
        }
    }
}
