using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiApp.DatabaseUitility;
using WebApiApp.Services;

namespace WebApiApp.Registration
{
    public static class Extensions
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services, IConfiguration _config)
        {
            services.AddScoped<IDatabaseContext, DatabaseContext>(_ => new DatabaseContext(_config["ConnectionStrings:MyDBConnection"].ToString()));
            services.AddScoped<CommonService>();

            return services;
        }
    }
}
