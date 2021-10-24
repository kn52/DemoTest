using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.AppConfig;
using RazorPages.DBContext;
using RazorPages.Entity;
using RazorPages.Entity.Product;
using RazorPages.Pagination;

namespace RazorPages.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;
        private readonly AppSettingConfig _appSettingConfig;

        public IndexModel(RazorDBContext context,VerifyLogin verifyLogin, AppSettingConfig appSettingConfig)
        {
            _verifyLogin = verifyLogin;
            _context = context;
            _appSettingConfig = appSettingConfig;
        }
        [BindProperty]
        public PaginatedList<Entity.Product.Category> Category { get;set; }
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
                var queryableList = _context.Categories.AsQueryable().Cast<Entity.Product.Category>();
                Category = await PaginatedList<Entity.Product.Category>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
            else
            {
                SearchTerm = SearchTerm.ToLower();
                var queryableList = _context.Categories.Where(c => c.Id.ToString().ToLower().Contains(SearchTerm) ||
                        c.Name.ToLower().Contains(SearchTerm) ||
                        c.Description.ToLower().Contains(SearchTerm));
                Category = await PaginatedList<Entity.Product.Category>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize);
            }

            return Page();
        }
    }
}
