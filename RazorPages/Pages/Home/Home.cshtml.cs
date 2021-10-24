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
using RazorPages.Entity.Home;
using RazorPages.Entity.Product;

namespace RazorPages.Pages.Home
{
    public class HomeModel : PageModel
    {
        public RazorDBContext _context { get; set;}
        private readonly VerifyLogin _verifyLogin;
        public HomeModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _context = context;
            _verifyLogin = verifyLogin;
        }

        [BindProperty]
        public IList<RedirectUrls> RedirectUrls { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            RedirectUrls = await _context.RedirectUrls.ToListAsync();
            ViewData["UrlDescription"] = new SelectList(RedirectUrls, "Url", "UrlDescription");
            
            ViewData["RedirectionMessage"] = "" + HomeMessage.GetHomeMessage();
            
            return Page();
        }

        public IActionResult OnPostAsync(string url)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (url != null)
            {
                return RedirectToPage(url);
            }
            
            return Page();
        }
    }
}
