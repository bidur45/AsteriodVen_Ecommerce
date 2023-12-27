using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static  class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ECommerceDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("DBConnection")));

            services.AddScoped<ITokenClaimService,TokenClaimService>();

            services.AddScoped(typeof(IUnitOfWork), u =>
            {
                var context = u.GetService<ECommerceDbContext>();
                return new UnitOfWork(context);
            });
        }
    }
}
