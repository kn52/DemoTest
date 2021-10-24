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
    public class DeleteUserModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;
        public DeleteUserModel(RazorDBContext context, VerifyLogin verifyLogin)
        {
            _context = context;
            _verifyLogin = verifyLogin;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDetails = await _context.UserDetails.FindAsync(id);

            if (UserDetails != null)
            {
                _context.UserDetails.Remove(UserDetails);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./DisplayUser");
        }
    }
}
