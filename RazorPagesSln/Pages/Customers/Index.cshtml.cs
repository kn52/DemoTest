using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesSln.AppConfig;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.CustomerMngt;
using RazorPagesSln.Pagination;

namespace RazorPagesSln.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;
        private readonly AppSettingConfig _appSettingConfig;

        public IndexModel(RazorDBContext context, VerifyLogin verifyLogin, AppSettingConfig appSettingConfig)
        {
            _verifyLogin = verifyLogin;
            _context = context;
            _appSettingConfig = appSettingConfig;
        }

        [BindProperty]
        public PaginatedList<Customer> Customer { get; set; }
        [BindProperty(SupportsGet = true, Name = "CurrentFilter")]
        public string SearchTerm { get; set; }
        [BindProperty]
        public IList<SortList> SortLists { get; set; }
        [BindProperty]
        public string CurrentSort { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex, string search, string sort)
        {
            int pageSize = _appSettingConfig._pageSize;
            if (search != null)
            {
                SearchTerm = search.ToLower();
            }
            if (sort != null)
            {
                CurrentSort = sort.ToLower();
            }
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            SortLists = SortCustomers.GetSortByList();
            var selectListItem = SortLists.FirstOrDefault(x => x.Key.ToLower() == CurrentSort);
            ViewData["Value"] = new SelectList(SortLists, "Key", "Value",selectListItem);

            IQueryable<Customer> queryableList = null;
            
            if (string.IsNullOrEmpty(SearchTerm) || string.IsNullOrEmpty(CurrentSort))
            {
                queryableList = _context.Customers.AsQueryable().Cast<Customer>();
            }
            else
            {
                queryableList = CurrentSort switch
                {
                    "name" => _context.Customers.Where(c => c.CustomerName.ToLower().Contains(SearchTerm)),
                    "contact" => _context.Customers.Where(c => c.CustomerContact.ToLower().Contains(SearchTerm)),
                    "phone" => _context.Customers.Where(c => c.CustomerPhone.ToLower().Contains(SearchTerm)),
                    "email" => _context.Customers.Where(c => c.CustomerEmail.ToLower().Contains(SearchTerm)),
                    _ => _context.Customers.AsQueryable().Cast<Customer>()
                };
            }

            if (queryableList.Any())
            {
                Customer = await PaginatedList<Customer>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
            HttpContext.Session.SetString("viewname", "All Customers");
            return Page();
        }

        public IActionResult OnPostLogoutEmpAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../Login/Userlogin");
        }
    }
}
