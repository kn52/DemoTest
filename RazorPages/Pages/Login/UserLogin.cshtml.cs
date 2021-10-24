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

namespace RazorPages.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public UserLoginModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        public IActionResult OnGet()
        {
            string name = HttpContext.Session.GetString("username");
            string pwd = HttpContext.Session.GetString("password");

            if (name != null || pwd != null)
            {
                return RedirectToPage("../Customers/Index");
            }

            return Page();
        }

        [BindProperty]
        public LoginCredentials LoginCredentials { get; set; }

        public IActionResult OnPostAsync()
        {
            if (IsValidCredentials())
            {
                _verifyLogin.AddLoginCredentials(LoginCredentials);
                return RedirectToPage("../Customers/Index");
            }
            
            return Page();
        }

        private bool IsValidCredentials()
        {
            return _context.LoginCredentials.Any(e => e.Username == LoginCredentials.Username && e.Password == LoginCredentials.Password); 
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../User/Userlogin");
        }
    }
}
