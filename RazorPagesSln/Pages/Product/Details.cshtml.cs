using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.Product;
using RazorPagesSln.Repository.Product;

namespace RazorPagesSln.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly VerifyLogin _verifyLogin;

        public DetailsModel(IProductRepository productRepository, VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public Entity.Product.Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (productId == null)
            {
                return NotFound();
            }

            Product = await _productRepository.GetProductByIdAsync(productId.Value);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
