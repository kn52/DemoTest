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
using RazorPages.Pagination;

namespace RazorPages.Pages.User
{
    public class DisplayUserModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly AppSettingConfig _appSettingConfig;
        private readonly VerifyLogin _verifyLogin ;
        public DisplayUserModel(RazorDBContext context,AppSettingConfig appSettingConfig, VerifyLogin verifyLogin)
        {
            _context = context;
            _appSettingConfig = appSettingConfig;
            _verifyLogin = verifyLogin;
        }

        [BindProperty]
        //public IList<UserDetails> UserDetails { get; set; }
        public PaginatedList<UserDetails> UserDetails { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; }
        public async Task<IActionResult> OnGetAsync(int? pageIndex,string search)
        {
            int pageSize = _appSettingConfig._pageSize;
            if(search != null)
            {
                CurrentFilter = search.ToLower();
            }

            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (string.IsNullOrEmpty(CurrentFilter))
            {
                var queryableusers = _context.UserDetails.AsQueryable().Cast<UserDetails>();
                //var userList = await PaginatedList<UserDetails>.CreateAsync(queryableusers.AsNoTracking(), 1, 3);
                //UserDetails = userList.ToList();
                UserDetails = await PaginatedList<UserDetails>.CreateAsync(queryableusers.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
            else
            {
                var queryableusers = _context.UserDetails
                    .Where(u => u.Name.ToLower().Contains(CurrentFilter) ||
                        u.Address.ToLower().Contains(CurrentFilter) ||
                        u.City.ToLower().Contains(CurrentFilter));
                //var userList = await PaginatedList<UserDetails>.CreateAsync(queryableusers.AsNoTracking(), 1, 3);
                //UserDetails = userList.ToList();
                UserDetails = await PaginatedList<UserDetails>.CreateAsync(queryableusers.AsNoTracking(), pageIndex ?? 1, pageSize);
            }

            return Page();
        }
    }
}
