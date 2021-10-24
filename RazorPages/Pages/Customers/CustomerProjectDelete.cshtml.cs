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
using RazorPages.Entity.CustomerMngt;

namespace RazorPages.Pages.Customers
{
    public class CustomerProjectDeleteModel : PageModel
    {
        private readonly RazorDBContext _context;
        private readonly VerifyLogin _verifyLogin;

        public CustomerProjectDeleteModel(RazorDBContext context,VerifyLogin verifyLogin)
        {
            _verifyLogin = verifyLogin;
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Project Project { get; set; }

        [BindProperty]
        public IList<PDescription> PDescription { get; set; }

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

            Project = await _context.Projects
                .Include(c => c.Customer)
                    .FirstOrDefaultAsync(p => p.ProjectId == id);

            PDescription = await _context.PDescriptions.ToListAsync();

            if (Project == null)
            {
                return NotFound();
            }

            Customer = Project.Customer;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (Project != null)
            {
                _context.Projects.Remove(Project);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./CustomerProjects", new { id = Project.Customer.CustomerId });
        }
    }
}
