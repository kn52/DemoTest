using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcApplication.LoggerModel;

namespace WebMvcApplication.Configs
{
    public static class Model_Extensions
    {
        public static IServiceCollection AddModelExtensions(this IServiceCollection services)
        {
            services.AddSingleton<Credentials.Credentials>();
            services.AddSingleton<LogInfo>();
            
            return services;
        }
    }
}
