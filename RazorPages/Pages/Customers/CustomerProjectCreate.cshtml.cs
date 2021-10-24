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
using RazorPages.Entity.CustomerMngt;

namespace RazorPages.Pages.Customers
{
    public class CustomerProjectCreateModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CustomerProjectCreateModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public IList<PDescription> PDescription { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!_verifyLogin.VerifyLoginCredentials())
            {
                return RedirectToPage("../Login/UserLogin");
            }

            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers
                .Include(c => c.Projects)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (Customer == null)
            {
                return NotFound();
            }

            PDescription = await _context.PDescriptions.ToListAsync();
           
            ViewData["PCode"] = new SelectList(PDescription,"PDescriptionId", "PCode");

            return Page();

        }

        [BindProperty]
        public Project Project { get; set; }

        [BindProperty]
        public PDescription PDescriptions { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Project.PD = PDescriptions.PDescriptionId;
            Project.CustomerId = Customer.CustomerId;
            
            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./CustomerProjects", new { id = Customer.CustomerId });
        }
    }
}
