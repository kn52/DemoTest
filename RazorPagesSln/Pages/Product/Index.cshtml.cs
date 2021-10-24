using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesSln.AppConfig;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.Product;
using RazorPagesSln.Pagination;
using RazorPagesSln.Repository.Product;

namespace RazorPagesSln.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly VerifyLogin _verifyLogin;
        private readonly RazorDBContext _context;
        private readonly AppSettingConfig _appSettingConfig;

        public IndexModel(RazorDBContext context, VerifyLogin verifyLogin, AppSettingConfig appSettingConfig)
        {
            _verifyLogin = verifyLogin;
            _context = context;
            _appSettingConfig = appSettingConfig;
        }

        public PaginatedList<Entity.Product.Product> ProductList { get; set; }

        [BindProperty(SupportsGet = true, Name = "CurrentFilter")]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex,string search)
        {
            int pageSize = _appSettingConfig._pageSize;
            if(search != null)
            {
                SearchTerm = search;
            }
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (string.IsNullOrEmpty(SearchTerm))
            {
                var queryableList = _context.Products.Include(p => p.Category).AsQueryable().Cast<Entity.Product.Product>();
                ProductList = await PaginatedList<Entity.Product.Product>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize); 
            }
            else
            {
                SearchTerm = SearchTerm.ToLower();
                var queryableList = _context.Products.Include(p => p.Category)
                    .Where(p => p.Id.ToString().ToLower().Contains(SearchTerm) ||
                            p.Name.ToLower().Contains(SearchTerm) ||
                            p.UnitPrice.ToString().ToLower().Contains(SearchTerm) ||
                            p.Category.Name.ToLower().Contains(SearchTerm));
                ProductList = await PaginatedList<Entity.Product.Product>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize);
            }

            return Page();
        }
    }
}
