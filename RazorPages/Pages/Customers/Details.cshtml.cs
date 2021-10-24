using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.DBContext;
using RazorPages.Entity;
using RazorPages.Entity.CustomerMngt;

namespace RazorPages.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public DetailsModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
