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
using RazorPagesSln.Repository.Product;

namespace RazorPagesSln.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly VerifyLogin _verifyLogin;

        public CreateModel(IProductRepository productRepository,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            var categories = await _productRepository.GetCategories();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Entity.Product.Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product = await _productRepository.AddAsync(Product);
            return RedirectToPage("./Index");
        }
    }
}
