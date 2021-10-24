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
using RazorPages.Entity.Product;
using RazorPages.Repository.Product;

namespace RazorPages.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly VerifyLogin _verifyLogin;

        public DetailsModel(IProductRepository productRepository,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [BindProperty]
        public IEnumerable<Entity.Product.Category> CategoryList { get; set; } = new List<Entity.Product.Category>();
        
       public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            CategoryList = await _productRepository.GetCategoryDetails(id);
            
            return Page();
        }
    }
}
