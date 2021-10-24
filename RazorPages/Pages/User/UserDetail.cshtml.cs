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

namespace RazorPages.Pages.User
{
    public class UserDetailModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public UserDetailModel(RazorDBContext context, VerifyLogin verifyLogin)
        {
            _context = context;
            _verifyLogin = verifyLogin;
        }

        public UserDetails UserDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (id == null)
            {
                return NotFound();
            }

            UserDetails = await _context.UserDetails.FirstOrDefaultAsync(m => m.Id == id);

            if (UserDetails == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
