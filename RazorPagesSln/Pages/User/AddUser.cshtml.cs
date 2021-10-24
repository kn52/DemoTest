using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSln.DBContext;
using RazorPagesSln.Entity;

namespace RazorPagesSln.Pages.User
{
    public class AddUserModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;
        public AddUserModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _context = context;
            _verifyLogin = verifyLogin;
        }

        public IActionResult OnGet()
        {
            if(!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            return Page();
        }

        [BindProperty]
        public UserDetails UserDetails { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UserDetails.Add(UserDetails);
            await _context.SaveChangesAsync();

            return RedirectToPage("./DisplayUser");
        }
    }
}
