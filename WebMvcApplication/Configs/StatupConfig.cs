using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcApplication.DbC;
using WebMvcApplication.LoggerModel;

namespace WebMvcApplication.ProjectSettings
{
    public static class StatupConfig
    {
        public static IServiceCollection AddStartupServices(this IServiceCollection services, IConfiguration _configuration)
        {
            string _dbConn = _configuration["ConnectionStrings:MyDBConnection"];

            services.AddSession();
            var loggerconfiguration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json")
                    .Build();
            var serilogLogger = Logger.LoggerFactory.Initialize(loggerconfiguration);
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSerilog(logger: serilogLogger, dispose: true);
            });
            
            services.AddDbContextPool<DBC>(options => options.UseMySql(_dbConn, ServerVersion.AutoDetect(_dbConn)));

            return services;
        }
    }
}
