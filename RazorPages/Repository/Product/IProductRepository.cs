using RazorPages.Entity.Product;
using RazorPages.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Repository.Product
{
    public interface IProductRepository
    {
        Task<PaginatedList<Entity.Product.Product>> GetProductListAsync(int? pageIndex);
        Task<Entity.Product.Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Entity.Product.Product>> GetProductByNameAsync(string name);
        Task<IEnumerable<Entity.Product.Product>> GetProductByPriceAsync(string price);
        Task<IEnumerable<Entity.Product.Product>> GetProductByCategoryAsync(int categoryId);
        Task<Entity.Product.Product> AddAsync(Entity.Product.Product product);
        Task UpdateAsync(Entity.Product.Product product);
        Task DeleteAsync(Entity.Product.Product product);
        Task<IEnumerable<Entity.Product.Category>> GetCategories();
        Task<IEnumerable<Entity.Product.Category>> GetCategoryDetails(int? id);
        Task<IEnumerable<Entity.Product.Product>> GetProductBySearchTerm(string searchTerm);
    }
}
