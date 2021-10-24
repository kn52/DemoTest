using Microsoft.EntityFrameworkCore;
using RazorPagesSln.AppConfig;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity.Product;
using RazorPagesSln.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        protected readonly RazorDBContext _dbContext;
        protected readonly AppSettingConfig _appSettingConfig;

        public ProductRepository(RazorDBContext dbContext, AppSettingConfig appSettingConfig)
        {
            _appSettingConfig = appSettingConfig;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PaginatedList<Entity.Product.Product>> GetProductListAsync(int? pageIndex)
        {
            int pageSize = _appSettingConfig._pageSize;
            var queryableList = _dbContext.Products.Include(p => p.Category);
            var returnList = await PaginatedList<Entity.Product.Product>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize);
            return returnList;
        }

        public async Task<Entity.Product.Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Entity.Product.Product>> GetProductByNameAsync(string name)
        {
            return await _dbContext.Products
                    .Include(p => p.Category)
                    .Where(p => string.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name.ToLower()))
                    .OrderBy(p => p.Name)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Entity.Product.Product>> GetProductByPriceAsync(string price)
        {
            return await _dbContext.Products
                    .Include(p => p.Category)
                    .Where(p => string.IsNullOrEmpty(price) || p.UnitPrice.ToString().Contains(price))
                    .OrderBy(p => p.UnitPrice)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Entity.Product.Product>> GetProductByCategoryAsync(int categoryId)
        {
            return await _dbContext.Products
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Entity.Product.Product> AddAsync(Entity.Product.Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Entity.Product.Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Entity.Product.Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryDetails(int? id)
        {
            return await _dbContext.Categories
               .Include(p => p.Products)
               .Where(x => x.Id == id)
               .ToListAsync();
        }

        public async Task<IEnumerable<Entity.Product.Product>> GetProductBySearchTerm(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return await _dbContext.Products.Include(p => p.Category)
                .Where(p => p.Id.ToString().ToLower().Contains(searchTerm) ||
                        p.Name.ToLower().Contains(searchTerm) ||
                        p.UnitPrice.ToString().ToLower().Contains(searchTerm) ||
                        p.Category.Name.ToLower().Contains(searchTerm))
                .ToListAsync();
        }
    }
}
