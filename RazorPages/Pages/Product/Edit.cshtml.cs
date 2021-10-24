using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPages.DBContext;
using RazorPages.Entity;
using RazorPages.Entity.Product;
using RazorPages.Repository.Product;

namespace RazorPages.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly VerifyLogin _verifyLogin;
        public EditModel(IProductRepository productRepository, VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }


        [BindProperty]
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

            ViewData["CategoryId"] = new SelectList(await _productRepository.GetCategories(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _productRepository.UpdateAsync(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            var product = _productRepository.GetProductByIdAsync(id);
            return product != null;
        }
    }
}
