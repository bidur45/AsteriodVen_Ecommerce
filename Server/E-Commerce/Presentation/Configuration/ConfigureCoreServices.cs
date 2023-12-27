using Application.Common;
using Application.Interfaces;
using Application.Interfaces.ECommerce;
using Application.Interfaces.Security;
using Application.Services.ECommerce;
using Application.Services.Security;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Presentation.Utils;

namespace Presentation.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddScoped<ECommerceDbContext>();
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
            services.AddAutoMapper(typeof(Program));
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();







            return services;
        }
    }
}
