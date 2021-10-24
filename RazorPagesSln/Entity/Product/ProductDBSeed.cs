using Microsoft.Extensions.Logging;
using RazorPagesSln.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity.Product
{
    public class ProductDBSeed
    {
        public static async Task SeedAsync(RazorDBContext dbContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                if (!dbContext.Categories.Any())
                {
                    dbContext.Categories.AddRange(GetPreconfiguredCategories());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Products.Any())
                {
                    dbContext.Products.AddRange(GetPreconfiguredProducts());
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<RazorDBContext>();
                    log.LogError(exception.Message);
                    await SeedAsync(dbContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category() { Name = "Phone", Description = "Smart Phones" },
                new Category() { Name = "TV", Description = "Television" },
                new Category() { Name = "Tab", Description = "Tablet" }
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product() { Name = "IPhone X", Description = "IPhone X Well Done", CategoryId = 1, UnitPrice = 52000 },
                new Product() { Name = "Samsung 10", Description = "Samsung X Well Done", CategoryId = 1, UnitPrice = 30000 },
                new Product() { Name = "LG 5", Description = "LG 5 Well Done", CategoryId = 1, UnitPrice = 25000 },
                new Product() { Name = "Huawei Plus", Description = "Huawei X Well Done", CategoryId = 2, UnitPrice = 27000 }
            };
        }
    }
}

