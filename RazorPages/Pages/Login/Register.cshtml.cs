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
    public class RegisterModel : PageModel
    {
        private readonly RazorPages.DBContext.RazorDBContext _context;

        public RegisterModel(RazorPages.DBContext.RazorDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginCredentials LoginCredentials { get; set; }

        public IActionResult OnGet()
        {
            string name = HttpContext.Session.GetString("username");
            string pwd = HttpContext.Session.GetString("password");

            if (name != null || pwd != null)
            {
                return RedirectToPage("../User/DisplayUser");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LoginCredentials).State = EntityState.Modified;

            try
            {
                if (!LoginCredentialsExists("1"))
                {
                    _context.LoginCredentials.Add(LoginCredentials);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToPage("./UserLogin");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginCredentialsExists(LoginCredentials.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Register");
        }

        private bool LoginCredentialsExists(string id)
        {
            return _context.LoginCredentials.Any(e => e.Id == id);
        }
    }
}
