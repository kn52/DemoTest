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
using RazorPagesSln.Entity.Home;

namespace RazorPagesSln.Pages.Home
{
    public class AddUrlsModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public AddUrlsModel(RazorDBContext context, VerifyLogin verifyLogin)
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
        public RedirectUrls RedirectUrls { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string name = HttpContext.Session.GetString("username");
            string password = HttpContext.Session.GetString("password");

            if(name != null || password != null)
            {
                _context.RedirectUrls.Add(RedirectUrls);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Home");
        }
    }
}
