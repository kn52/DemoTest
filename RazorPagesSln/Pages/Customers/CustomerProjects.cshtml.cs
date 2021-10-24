using System;
using System.Collections;
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
using RazorPagesSln.Entity.CustomerMngt;
using RazorPagesSln.Pagination;

namespace RazorPagesSln.Pages.Customers
{
    public class CustomerProjectsModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;
        private readonly AppSettingConfig _appSettingConfig;

        public CustomerProjectsModel(RazorDBContext context,VerifyLogin verifyLogin, AppSettingConfig appSettingConfig)
        {
            _verifyLogin = verifyLogin;
            _context = context;
            _appSettingConfig = appSettingConfig;
        }

        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public PaginatedList<Project> Projects { get; set; }
        [BindProperty]
        public IList<PDescription> PDescription { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id,int? pageIndex)
        {
            int pageSize = _appSettingConfig._pageSubSize;
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            var queryableList = _context.Projects.Where(p=>p.CustomerId == Customer.CustomerId);
            Projects = await PaginatedList<Project>.CreateAsync(queryableList.AsNoTracking(), pageIndex ?? 1, pageSize);

            PDescription = await _context.PDescriptions.ToArrayAsync();

            if (Customer == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("viewname", "Customer Projects");
            return Page();
        }
    }
}
