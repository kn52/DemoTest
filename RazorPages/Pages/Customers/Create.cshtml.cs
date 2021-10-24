using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPages.DBContext;
using RazorPages.Entity;
using RazorPages.Entity.CustomerMngt;

namespace RazorPages.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CreateModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
