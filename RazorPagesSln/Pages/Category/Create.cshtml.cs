using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.Product;

namespace RazorPagesSln.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CreateModel(RazorDBContext context, VerifyLogin verifyLogin)
        {
            _context = context;
            _verifyLogin = verifyLogin;
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
        public Entity.Product.Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
