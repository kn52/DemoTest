using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using RazorPages.DBContext;
using RazorPages.Entity;
using RazorPages.Repository.Product;
using System;

namespace RazorPages.AppConfig
{
    public static class StartupConfig
    {
        public static IServiceCollection AddStartupServices(this IServiceCollection services, IConfiguration _configuration)
        {
            string _dbConn = _configuration["ConnectionStrings:MyDBConnection"];
            
            //Dependency
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<VerifyLogin>();

            //DB Connection
            services.AddDbContextPool<RazorDBContext>(options => options.UseMySql(_dbConn,ServerVersion.AutoDetect(_dbConn)));
            
            //Session
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });
            

            //App Setting
            services.AddScoped<AppSettingConfig>();

            return services;
        }
    }
}
